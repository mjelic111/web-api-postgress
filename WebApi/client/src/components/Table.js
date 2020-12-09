import { Table } from "reactstrap";

const TableWrapper = ({ data }) => {
  return (
    <Table responsive>
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Address</th>
          <th>Birth date</th>
          <th>Telephone</th>
        </tr>
      </thead>
      <tbody>
        {data.map((client) => (
          <tr key={client.id}>
            <th scope="row">{client.id}</th>
            <td>{client.name}</td>
            <td>{client.address}</td>
            <td>{client.birthDate}</td>
            <td>{client.telephoneNumbers.join()}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};

export default TableWrapper;
