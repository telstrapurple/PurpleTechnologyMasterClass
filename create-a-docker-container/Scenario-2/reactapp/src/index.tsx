import React, { useState } from "react";
import ReactDOM from "react-dom";
import style from "./app.module.scss";
import useWebSockets from "./hooks/ws";

export const App = () => {
  const [message, setMessage] = useState("");
  const [log, setLog] = useState("");
  const [messageReceived, setMessageReceived] = useState<Array<string>>([]);
  const ws = useWebSockets((event) => {
    setMessageReceived([...messageReceived, event.data]);
  });

  const submit = async (event: React.MouseEvent<HTMLButtonElement>) => {
    var result = await fetch(
      process.env.REACT_APP_SERVICES_PUBLISH + encodeURIComponent(message)
    );

    setLog(await result.text());
  };

  return (
    <div className={style.container}>
      <h1>Send a message</h1>
      <div className={style.input}>
        <input
          type="text"
          placeholder="Enter a message"
          onChange={(event) => setMessage(event.currentTarget.value)}
          name="message"
        ></input>
      </div>
      <div>
        <button type="button" onClick={submit}>
          Send Message
        </button>
      </div>
      <div>
        <pre>{JSON.stringify(messageReceived, null, 2)}</pre>
      </div>
    </div>
  );
};

ReactDOM.render(<App />, document.getElementById("app"));
