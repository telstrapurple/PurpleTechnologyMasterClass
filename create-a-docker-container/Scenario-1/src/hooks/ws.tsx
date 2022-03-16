import { useEffect, useState } from "react";

export default function useWebSockets() {
  const [ws, setWS] = useState<WebSocket>();

  useEffect(() => {
    if (ws) {
      return;
    }

    const connection = new WebSocket(process.env.REACT_APP_SERVICES_SUBSCRIBE);

    ws.onopen = function (evt) {
      console.log("OPEN");
    };

    ws.onclose = function (evt) {
      console.log("CLOSE");
      setWS(null);
    };

    ws.onmessage = function (evt) {
      console.log("RESPONSE: " + evt.data);
    };
    ws.onerror = function (evt: any) {
      console.log("ERROR: " + evt.data);
    };

    setWS(connection);
  }, []);

  return ws;
}
