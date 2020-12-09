import { Button, Form, FormGroup, Label, Input, Col, Row } from "reactstrap";
import { useState } from "react";

const CreateClient = ({ createContact }) => {
  const [name, setName] = useState("");
  const [address, setAddress] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [tel1, setTel1] = useState("");
  const [tel2, setTel2] = useState("");
  const [tel3, setTel3] = useState("");

  return (
    <Form
      onSubmit={(e) => {
        e.preventDefault();
        const telephoneNumbers = [];
        if (tel1) telephoneNumbers.push(tel1);
        if (tel2) telephoneNumbers.push(tel2);
        if (tel3) telephoneNumbers.push(tel3);
        createContact({ name, address, birthDate, telephoneNumbers });
      }}
    >
      <FormGroup>
        <Label for="name">Name</Label>
        <Input
          type="text"
          name="name"
          id="name"
          onChange={(e) => setName(e.target.value)}
        />
      </FormGroup>
      <FormGroup>
        <Label for="address">Address</Label>
        <Input
          type="text"
          name="address"
          id="address"
          onChange={(e) => setAddress(e.target.value)}
        />
      </FormGroup>
      <FormGroup>
        <Label for="birthDateId">Date</Label>
        <Input
          type="date"
          name="birthDate"
          id="birthDateId"
          placeholder="date placeholder"
          onChange={(e) => setBirthDate(e.target.value)}
        />
      </FormGroup>
      <FormGroup>
        <Label>Telephone numbers</Label>
      </FormGroup>
      <Row form>
        <Col>
          <FormGroup>
            <Input
              type="text"
              name="home"
              id="home"
              placeholder="home"
              onChange={(e) => setTel1(e.target.value)}
            />
          </FormGroup>
        </Col>
        <Col>
          <FormGroup>
            <Col>
              <Input
                type="text"
                name="work"
                id="work"
                placeholder="work"
                onChange={(e) => setTel2(e.target.value)}
              />
            </Col>
          </FormGroup>
        </Col>
        <Col>
          <FormGroup>
            <Col>
              <Input
                type="text"
                name="mobile"
                id="mobile"
                placeholder="mobile"
                onChange={(e) => setTel3(e.target.value)}
              />
            </Col>
          </FormGroup>
        </Col>
      </Row>

      <Button>Submit</Button>
    </Form>
  );
};

export default CreateClient;
