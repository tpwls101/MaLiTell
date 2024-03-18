package com.ssafy.malitell.domain.user;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import com.ssafy.malitell.domain.selfhelpgroup.SelfHelpGroupUser;
import com.ssafy.malitell.domain.selfhelpgroup.SelfHelpType;
import com.ssafy.malitell.domain.tag.StatusTag;
import com.ssafy.malitell.dto.request.auth.SignUpRequestDto;
import com.ssafy.malitell.dto.request.user.ClientUpdateRequestDto;
import com.ssafy.malitell.dto.request.user.CounselorUpdateRequestDto;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.io.Serial;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

@Entity
@Setter
@Getter
@AllArgsConstructor
@NoArgsConstructor
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int userSeq; // idx
    @NotNull
    private String userId; // 아이디
    @NotNull
    private String name; // 이름
    private String nickname; // 닉네임
    private String password; // 비밀번호
    @NotNull
    private String email; // 이메일
    @NotNull
    private String phone; // 핸드폰 번호
    @NotNull
    private String birth; // 생년월일
    @NotNull
    private String gender; // 성별
    @NotNull
    private String role; // 권한 (counselor, client, admin)
    private String refreshToken; // refresh token

    @Column(columnDefinition = "TEXT")
    private String profileImg; // 프로필 사진
    private String professionalField; // 전문분야
    private int careerPeriod; // 경력
    private double grade; // 평점
    private String comment; // 소개 멘트
    private String type; // 타입 (kakao, naver)

    private String alramMessage;
    private int readCheck; // 안 읽었으면 1 , 읽었으면 0

    private String educationField; // 학력사항
    private String certificateField; // 자격증

    // 참가하고 있는 자조모임
    @JsonManagedReference(value = "userrrrrrrr")
    @OneToMany(mappedBy = "user")
    private List<SelfHelpGroupUser> selfHelpGroupUsers = new ArrayList<>();

    @ElementCollection
    private List<StatusTag> statusTags = new ArrayList<>();

    public void addSelfHelpGroupUsers(SelfHelpGroupUser selfHelpGroupUser) {
        selfHelpGroupUsers.add(selfHelpGroupUser);
        selfHelpGroupUser.setUser(this);
    }

    public void removeSelfHelpGroupUsers(SelfHelpGroupUser selfHelpGroupUser) {
        selfHelpGroupUsers.remove(selfHelpGroupUser);
        selfHelpGroupUser.setUser(null);
    }

    public void updateProfileImg(String profileImg) {
        this.profileImg = profileImg;
    }

    public void addTag(List<StatusTag> statusTags) {
        this.statusTags = statusTags;
    }


    public User(SignUpRequestDto dto) {
        this.userId = dto.getUserId();
        this.password = dto.getPassword();
        this.email = dto.getEmail();
        this.type = "app";
        this.role = "ROLE_CLIENT";
    }

    public User(String userId, String name, String nickname, String email, String phone, String birth, String gender, String role, String type) {
        this.userId = userId;
        this.name = name;
        this.nickname = nickname;
        this.email = email;
        this.phone = phone;
        this.birth = birth;
        this.role = role;
        this.type = type;
        this.gender = gender;
    }

    public void updateClient(ClientUpdateRequestDto clientUpdateRequestDto) {
        this.name = clientUpdateRequestDto.getName();
        this.email = clientUpdateRequestDto.getEmail();
        this.phone = clientUpdateRequestDto.getPhone();
        this.profileImg = clientUpdateRequestDto.getProfileImg();
        List<String> statusTags = clientUpdateRequestDto.getStatusTags();
        this.statusTags.clear();
        for (String statusTag : statusTags) {
            this.statusTags.add(StatusTag.valueOf(statusTag));
        }
    }

    public void updateCounselor(CounselorUpdateRequestDto counselorUpdateRequestDto) {
        this.name = counselorUpdateRequestDto.getName();
        this.email = counselorUpdateRequestDto.getEmail();
        this.phone = counselorUpdateRequestDto.getPhone();
        this.careerPeriod = counselorUpdateRequestDto.getCareerPeriod();
        this.profileImg = counselorUpdateRequestDto.getProfileImg();
        this.professionalField = counselorUpdateRequestDto.getProfessionalField();
        this.comment = counselorUpdateRequestDto.getComment();
        this.statusTags.clear();
        List<String> statusTagList = counselorUpdateRequestDto.getStatusTags();
        for (String statusTag : statusTagList) {
            this.statusTags.add(StatusTag.valueOf(statusTag));
        }
    }

    public void updatePassword(String password) {
        this.password = password;
    }

    @Override
    public String toString() {
        return "User{" +
                "userSeq=" + userSeq +
                ", userId='" + userId + '\'' +
                ", name='" + name + '\'' +
                ", nickname='" + nickname + '\'' +
                ", password='" + password + '\'' +
                ", email='" + email + '\'' +
                ", phone='" + phone + '\'' +
                ", birth='" + birth + '\'' +
                ", gender='" + gender + '\'' +
                ", role='" + role + '\'' +
                ", refreshToken='" + refreshToken + '\'' +
                ", profileImg='" + profileImg + '\'' +
                ", professionalField='" + professionalField + '\'' +
                ", careerPeriod=" + careerPeriod +
                ", grade=" + grade +
                ", comment='" + comment + '\'' +
                ", type='" + type + '\'' +
                ", alramMessage='" + alramMessage + '\'' +
                ", readCheck=" + readCheck +
                ", selfHelpGroupUsers=" + selfHelpGroupUsers +
                ", statusTags=" + statusTags +
                '}';
    }
}
