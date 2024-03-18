import React, {
  HtmlHTMLAttributes,
  ReactHTML,
  useEffect,
  useState,
} from "react";
import * as s from "../../../styles/article/createForm";
import { useSelector } from "react-redux";
import { RootState } from "../../../store/store";
import {
  SelfHelpGroupForm,
  createSHGroup,
} from "../../../store/article/gatherSlice";
import { useNavigate } from "react-router-dom";

const tagList = ["진로","정서","대인관계","경제","건강"];
const tagEngList = ["COURSE", "EMOTION", "RELATIONSHIP", "ECONOMY", "HEALTH"]
const today = new Date();
const year = today.getFullYear();
const month = String(today.getMonth() + 1).padStart(2, '0'); // 월은 0부터 시작하므로 1을 더해줍니다.
const day = String(today.getDate()).padStart(2, '0');

const todayStr = `${year}-${month}-${day}`;

export default function CreateGatherForm() {
  
  const boardType = useSelector((state: RootState) => state.board.boardType);
  const navigate = useNavigate();
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [selfHelpGroupTitle, setSelfHelpGroupTitle] = useState("");
  const [selfHelpType, setSelfHelpType] = useState("진로");
  const [worryTag, setWorryTag] = useState("")
  const [headcount, setHeadcount] = useState(5);
  const [startDate, setStartDate] = useState(todayStr);
  const [startTime, setStartTime] = useState("00:00");
  const [totalCount, setTotalCount] = useState(1);

  const handleTagClick = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedIndex = e.target.selectedIndex - 1;
    setSelfHelpType(tagList[selectedIndex]);
    setWorryTag(tagEngList[selectedIndex]);
  };

  const handleTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(e.target.value);
  };

  const handleContentChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setContent(e.target.value);
  };

  const handleHeadCountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setHeadcount(Number(e.target.value));
  };

  const handleGroupTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSelfHelpGroupTitle(e.target.value);
  };

  const handleStartDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setStartDate(e.target.value);
  };

  const handleStartTimeChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setStartTime(e.target.value);
  };

  const handleTotalCount = (e: React.ChangeEvent<HTMLInputElement>) => {
    setTotalCount(Number(e.target.value));
  };
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const dateTimeStr = `${startDate}T${startTime}`;
    const startTimestamp = new Date(dateTimeStr).getTime();

    const times = Array.from(
      { length: totalCount },
      (value, i) => startTimestamp + i * 604800000
    );
    const res = await createSHGroup({
      selfHelpGroupTitle,
      selfHelpGroupContent: content,
      times: times,
      selfHelpType,
      headcount,
      title,
      content,
    });
    if (res) {
      // navigate(`/articles/${boardType}/생성한 게시글`)
      navigate(`/articles/${boardType}`);
    }
    // 잘못 요청되었다는 메시지
  };

  useEffect(() => {
  }, [boardType]);

  return (
    <s.Wrapper onSubmit={handleSubmit}>
      <s.TopBox>
        <s.TagBox>
          <s.Label>주제: </s.Label>
          <s.Select onChange={handleTagClick}>
            <s.Option defaultChecked hidden>
              선택
            </s.Option>
            {tagList.map((tagName, index) => (
              <s.Option key={index} value={tagName}>
                {tagName}
              </s.Option>
            ))}
          </s.Select>
        </s.TagBox>
        <s.Title
          value={title}
          onChange={handleTitleChange}
          placeholder="게시글 제목"
        />
        <s.GroupTitle
          value={selfHelpGroupTitle}
          onChange={handleGroupTitleChange}
          placeholder="자조모임 제목"
        />
        <s.DateContainer>
          <s.DateBox>
            <s.DateLabel>인원: </s.DateLabel>
            <input
              type="number"
              value={headcount}
              onChange={handleHeadCountChange}
              min="5"
              max="10"
            />
          </s.DateBox>
          <s.DateBox>
            <s.DateLabel>시작 날짜: </s.DateLabel>
            <input
              type="date"
              onChange={handleStartDateChange}
              value={startDate}
            />
          </s.DateBox>
          <s.DateBox>
            <s.DateLabel>시작 시간: </s.DateLabel>
            <input
              type="time"
              onChange={handleStartTimeChange}
              value={startTime}
            />
          </s.DateBox>
          <s.DateBox>
            <s.DateLabel>총 회차: </s.DateLabel>
            <input
              type="number"
              onChange={handleTotalCount}
              value={totalCount}
              min="1"
              max="5"
            />
          </s.DateBox>
        </s.DateContainer>
      </s.TopBox>
      <s.Input
        value={content}
        onChange={handleContentChange}
        placeholder="내용"
      />
      <s.Submit type="submit" />
    </s.Wrapper>
  );
}
