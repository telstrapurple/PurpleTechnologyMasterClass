package main

import (
	"context"
	"flag"
	"fmt"
	"log"
	"net/http"
	"time"

	"github.com/gorilla/websocket"
	influxdb2 "github.com/influxdata/influxdb-client-go/v2"
	"github.com/streadway/amqp"
)

var upgrader = websocket.Upgrader{
	ReadBufferSize:  4096,
	WriteBufferSize: 4096,
	CheckOrigin: func(r *http.Request) bool {
		return true
	}} // use default options

var wsConn *websocket.Conn

func homePage(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Welcome to the HomePage!")
	fmt.Println("Endpoint Hit: homePage")
}

func echo(w http.ResponseWriter, r *http.Request) {
	var err error

	wsConn, err = upgrader.Upgrade(w, r, nil)

	log.Print("connecting to websocket")

	if err != nil {
		log.Print("upgrade:", err)
		return
	}

	go func() {
		for {
			mt, message, err := wsConn.ReadMessage()
			if err != nil {
				log.Println("read:", err)
				break
			}
			log.Printf("recv: %s", message)
			err = wsConn.WriteMessage(mt, message)
			if err != nil {
				log.Println("write:", err)
				break
			}
		}
	}()

	log.Printf(" [*] Waiting for websocket messages. To exit press CTRL+C")

}

func main() {

	flag.Parse()
	log.SetFlags(0)
	http.HandleFunc("/wsConnect", echo)
	http.HandleFunc("/", homePage)

	go listen()

	err := http.ListenAndServe(":10001", nil)
	failOnError(err, "Server")
}

func failOnError(err error, msg string) {
	if err != nil {
		log.Fatalf("%s: %s", msg, err)
		log.Printf("%s: %s", msg, err)
	}
}

type withRetry func()

func failOnErrorWithRetry(err error, msg string, retry withRetry) {
	if err != nil {
		log.Printf("%s: %s", msg, err)
		retry()
	}
}

func listen() {
	log.Print("listening to rabbit")
	conn, err := amqp.Dial("amqp://guest:guest@rabbitmq-container:5672/")
	failOnError(err, "Failed to connect to RabbitMQ")
	defer conn.Close()

	ch, err := conn.Channel()
	failOnError(err, "Failed to open a channel")
	defer ch.Close()

	q, err := ch.QueueDeclare(
		"queue-1", // name
		false,     // durable
		false,     // delete when unused
		false,     // exclusive
		false,     // no-wait
		nil,       // arguments
	)
	failOnError(err, "Failed to declare a queue")

	msgs, err := ch.Consume(
		q.Name, // queue
		"",     // consumer
		true,   // auto-ack
		false,  // exclusive
		false,  // no-local
		false,  // no-wait
		nil,    // args
	)
	failOnError(err, "Failed to register a consumer")

	forever := make(chan bool)

	go func() {
		for d := range msgs {
			log.Printf("Received a message: %s", d.Body)

			err := wsConn.WriteMessage(websocket.TextMessage, []byte([]byte(d.Body)))

			failOnError(err, "failed to write websocket message")

		}
	}()

	log.Printf(" [*] Waiting for messages. To exit press CTRL+C")

	<-forever
}

// This is not called.
func writeToInflux() {
	bucket := "data-bucket"
	org := "data-org"
	token := "data-token"
	// Store the URL of your InfluxDB instance
	url := "http://localhost:8086"
	// Create new client with default option for server url authenticate by token
	client := influxdb2.NewClient(url, token)
	// User blocking write client for writes to desired bucket
	writeAPI := client.WriteAPIBlocking(org, bucket)
	// Create point using full params constructor
	p := influxdb2.NewPoint("stat",
		map[string]string{"unit": "temperature"},
		map[string]interface{}{"avg": 24.5, "max": 45},
		time.Now())
	// Write point immediately
	err := writeAPI.WritePoint(context.Background(), p)
	// Ensures background processes finishes

	// Get query client
	queryAPI := client.QueryAPI("my-org")
	// Get parser flux query result
	result, err := queryAPI.Query(context.Background(), `from(bucket:"data-bucket")|> range(start: -1h) |> filter(fn: (r) => r._measurement == "stat")`)

	if err == nil {
		log.Println(result)
	}

	client.Close()
}
