import { useEffect, useState } from "react";

export default function useWebSockets(
  onmessage?: (event: MessageEvent<any>) => void
) {
  const [ws, setWS] = useState<WebSocket>();

  useEffect(() => {
    if (ws) {
      return;
    }

    const connection = new WebSocket(process.env.REACT_APP_SERVICES_WS);

    if (!connection) throw "ws connection not set";

    connection.onmessage = (ev: MessageEvent<any>) => {
      console.log("message received", ev);
      onmessage && onmessage(ev.data);
    };

    connection.onopen = function (evt) {
      console.log("OPEN");
    };

    connection.onclose = function (evt) {
      console.log("CLOSE");
      setWS(null);
    };
    connection.onmessage = function (evt) {
      console.log("RESPONSE: " + evt.data);
    };
    connection.onerror = function (evt: any) {
      console.log(evt);
    };

    setWS(connection);
  });

  return ws;
}
