import { Toast, ToastBody, ToastHeader } from "reactstrap";
import { useState } from "react";

const ToastWrapper = ({ title, message }) => {
  const [show, setShow] = useState(true);

  return <p>Toast</p>;
};

export default ToastWrapper;
