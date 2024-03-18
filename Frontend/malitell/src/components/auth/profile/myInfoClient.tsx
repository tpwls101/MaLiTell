import { useEffect, useRef, useState } from "react";
import * as s from "../../../styles/auth/profile/myInfo";
import {
  editClientInfo,
  editCounselorInfo,
  fetchUserInfo,
} from "../../../store/auth/profileSlice";
import { malitell } from "../../../assets/images/malitell.png";
export interface editUserData {
  userId: string;
  name: string;
  nickname: string;
  email: string;
  phone: string;
  birth: string;
  gender: string | null;
  role: string | null;
  profileImg: string | null;
  statusTags: string[];
  comment: null;
  careerPeriod: string | null;
  professionalField: string | null;
}
const clientTags = ["우울", "불안", "공황", "자존감"];
const counselorTags = ["다정한", "진실성있는", "경청하는", "적극적인코칭"];
export default function MyInfoClient() {
  const [profileImage, setProfileImage] = useState(null);
  const [userData, setUserData] = useState<editUserData>();
  const [selectedTags, setSelectedTags] = useState<string[]>([]);
  const fileInputRef = useRef<HTMLInputElement>(null); // 파일 입력 필드 참조 생성

  const handleTagClick = (tag: string) => {
    if (selectedTags.includes(tag)) {
      // 이미 선택된 태그를 클릭한 경우, 태그를 해제
      setSelectedTags(selectedTags.filter((t) => t !== tag));
    } else {
      // 선택되지 않은 태그를 클릭한 경우, 태그를 선택
      setSelectedTags([...selectedTags, tag]);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (userData) {
      setUserData({
        ...userData,
        [name]: value,
      });
    }
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files.length > 0) {
      const file = e.target.files[0];
      const reader = new FileReader();
      reader.onloadend = () => {
        if (userData) {
          setUserData({
            ...userData,
            profileImg: reader.result as string,
          });
        }
      };
      reader.readAsDataURL(file);
    }
  };

  const clearFileInput = () => {
    if (fileInputRef.current) {
      // 파일 입력 필드 참조가 존재하는 경우
      fileInputRef.current.value = ""; // 파일 입력 필드 초기화
    }
  };

  const handleEditProfile = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (userData) {
      let updatedProfileImg = userData.profileImg;
      if (!userData.profileImg) {
        updatedProfileImg = profileImage;
      }
      const updatedUserData = {
        ...userData,
        profileImg: updatedProfileImg,
        statusTags: selectedTags,
      };

      if (userData.role === "ROLE_CLIENT") {
        editClientInfo(updatedUserData);
      } else {
        editCounselorInfo(updatedUserData);
      }
    }
  };

  useEffect(() => {
    fetchUserInfo().then((res) => {
      setUserData(res);
      setProfileImage(res.profileImg || null);
      setSelectedTags(res.statusTags);
    });
  }, []);

  return (
    <s.Wrapper onSubmit={handleEditProfile}>
      {userData && (
        <>
          <s.DisableContents>
            <s.FlexBox>
              <div style={{ display: "flex" }}>
                <s.Subtitle $width="55px">이름: </s.Subtitle>
                <s.Info>{userData.name}</s.Info>
              </div>
              <div style={{ display: "flex" }}>
                <s.Subtitle $width="40px">ID: </s.Subtitle>
                <s.Info>{userData.userId}</s.Info>
              </div>
              <div style={{ display: "flex" }}>
                <s.Subtitle $width="80px">닉네임: </s.Subtitle>
                <s.Info>{userData.nickname}</s.Info>
              </div>
            </s.FlexBox>
          </s.DisableContents>

          <s.FileBox>
            <s.Subtitle>프로필 사진: </s.Subtitle>
            <s.Box
              type="file"
              name="profileImg"
              onChange={handleFileChange}
              ref={fileInputRef}
            />
            <s.Cancle type="button" onClick={clearFileInput}>
              취소
            </s.Cancle>
          </s.FileBox>
          <s.FixBox>
            <s.Subtitle>이메일</s.Subtitle>
            <s.Box
              type="text"
              name="email"
              value={userData.email}
              onChange={handleInputChange}
            />
            <s.Subtitle>핸드폰</s.Subtitle>
            <s.Box
              type="text"
              name="phone"
              value={userData.phone}
              onChange={handleInputChange}
            />
            <s.Subtitle>생년월일</s.Subtitle>
            <s.Box type="text" name="birth" value={userData.birth} disabled />
          </s.FixBox>
          {userData.role === "ROLE_CLIENT" ? (
            <>
              <s.TagBox>
                <s.Subtitle>나의 태그</s.Subtitle>
                {/* clientTags를 이용해서 태그들을 만들고 클릭 시 UserData.statusTags의 상태를 변화시켜줌 
                지금 4개의 상태가 있는데 각 버튼들이 있고 선택된 버튼은 선택되지 않은 버튼과 볼 때 차이가 있도록 한다.
                클릭을 하면 선택 상태가 토글이 되고 UserData.statusTags를 변화시킨다.
              */}
                <s.ButtonBox>
                  {clientTags.map((tag, index) => (
                    <s.TagButton
                      key={index}
                      type="button"
                      onClick={() => handleTagClick(tag)}
                      className={selectedTags.includes(tag) ? "select" : ""}
                    >
                      {tag}
                    </s.TagButton>
                  ))}
                </s.ButtonBox>
              </s.TagBox>
              <s.Submit type="submit">회원정보 수정</s.Submit>
            </>
          ) : (
            <>
              <div>
                {/* clientTags를 이용해서 태그들을 만들고 클릭 시 UserData.statusTags의 상태를 변화시켜줌 
                지금 4개의 상태가 있는데 각 버튼들이 있고 선택된 버튼은 선택되지 않은 버튼과 볼 때 차이가 있도록 한다.
                클릭을 하면 선택 상태가 토글이 되고 UserData.statusTags를 변화시킨다.
              */}
                {counselorTags.map((tag, index) => (
                  <button
                    key={index}
                    type="button"
                    onClick={() => handleTagClick(tag)}
                    style={{
                      fontWeight: selectedTags.includes(tag)
                        ? "bold"
                        : "normal",
                    }}
                  >
                    {tag}
                  </button>
                ))}
              </div>
              <div>상담가 전용</div>
              <div>상담가 소개</div>
              <s.Box
                type="string"
                name="comment"
                value={userData.comment || ""}
                onChange={handleInputChange}
              />
              <div>상담 경력</div>
              <s.Box
                type="number"
                name="careerPeriod"
                value={userData.careerPeriod || 0}
                onChange={handleInputChange}
              />
              <div>상담 분야</div>
              <s.Box
                type="text"
                name="professionalField"
                value={userData.professionalField || ""}
                onChange={handleInputChange}
              />
              <s.Box type="submit" value={"상담가정보 수정"} />
            </>
          )}
        </>
      )}
    </s.Wrapper>
  );
}
