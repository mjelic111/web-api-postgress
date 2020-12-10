import { Container, Row } from "reactstrap";
import { useState, useEffect, useCallback } from "react";
import "./Clients.css";
import Pagination from "./components/Pagination";
import Table from "./components/Table";
import CreateClient from "./components/CreateClient";
import toast from "react-simple-toasts";
import { HubConnectionBuilder } from "@microsoft/signalr";

function Clients() {
  const [loading, setLoading] = useState(true);
  const [data, setData] = useState([]);
  const [next, setNext] = useState({});
  const [previous, setPrevious] = useState({});
  const [totalPages, setTotalPages] = useState({});
  const [connection, setConnection] = useState(null);
  const [update, setUpdate] = useState({});
  const [url, setUrl] = useState(
    process.env.REACT_APP_API_URL + "api/contact/get/1"
  );

  useEffect(() => {
    async function fetchData() {
      const response = await fetch(url);
      const { data, next, previous, totalPages } = await response.json();
      setData(data);
      setNext(next);
      setPrevious(previous);
      setTotalPages(totalPages);
      setLoading(false);
    }
    fetchData();
  }, [url, update]);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(process.env.REACT_APP_API_URL + "contactHub")
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("Connected!");

          connection.on("ReceiveContact", (id, contact) => {
            console.log("ReceiveContact", id, contact);
            toast(`SignalR received contact: ${contact.name}`, 5000);
            setUpdate({}); //trigger refetch
          });
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  const createContact = useCallback(
    async (data) => {
      // console.log("Creating client...", data);
      const url = process.env.REACT_APP_API_URL + "api/contact";
      try {
        const response = await fetch(url, {
          method: "POST",
          body: JSON.stringify(data),
          headers: { "Content-type": "application/json; charset=UTF-8" },
        });
        await response.json();
        toast("Client created", 1500);
        // send signalR message
        if (connection.connectionStarted) {
          try {
            await connection.send("UpdateContact", 1, data);
          } catch (e) {
            console.log(e);
          }
        } else {
          alert("No connection to server yet.");
        }
      } catch (error) {
        console.log(error);
        toast("Error happend while creating client", 1500);
      }
    },
    [connection, data]
  );

  if (loading) {
    return (
      <p>
        <em>Loading...</em>
      </p>
    );
  }

  return (
    <>
      <Container>
        <Row>
          <h1>Simple Client Data</h1>
        </Row>
        <Row>
          <Table data={data} />
          <Pagination
            next={next}
            previous={previous}
            totalPages={totalPages}
            setUrl={setUrl}
          />
        </Row>
        <Row>
          <h2>Create new client</h2>
        </Row>
        <Row>
          <CreateClient createContact={createContact} />
        </Row>
      </Container>
    </>
  );
}

export default Clients;
