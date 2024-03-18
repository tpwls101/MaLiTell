import { useEffect, useState } from "react";
import { Wrapper } from "../../styles/bamboo/subject";

export default function Subject() {
  const [subject, setSubject] = useState();

  useEffect(() => {
    fetch(`https://i10c208.p.ssafy.io/api/mindLetGo/topic`, {
      method: "GET",
    })
      .then((res) => {
        return res.json();
      })
      .then((data) => {
        setSubject(data.topic);
      });
  });
  return <Wrapper>{subject}</Wrapper>;
}
