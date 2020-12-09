import { Container, Row } from "reactstrap";
import { useState, useEffect } from "react";
import "./Clients.css";
import Pagination from "./components/Pagination";
import Table from "./components/Table";
import CreateClient from "./components/CreateClient";

function Clients() {
  const [loading, setLoading] = useState(true);
  const [data, setData] = useState([]);
  const [next, setNext] = useState({});
  const [previous, setPrevious] = useState({});
  const [totalPages, setTotalPages] = useState({});

  const [url, setUrl] = useState(
    process.env.REACT_APP_API_URL + "contact/get/1"
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
  }, [url]);
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
          <CreateClient />
        </Row>
      </Container>
    </>
  );
}

export default Clients;
