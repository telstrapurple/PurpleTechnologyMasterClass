#!/bin/bash

FROM golang:alpine as builder

WORKDIR /app 

COPY . .

RUN CGO_ENABLED=0 GOOS=linux GOARCH=arm64 go build -ldflags="-w -s" .

FROM scratch 

WORKDIR /app

COPY --from=builder /app /usr/bin/

EXPOSE 10001

ENTRYPOINT ["subscriber"]
