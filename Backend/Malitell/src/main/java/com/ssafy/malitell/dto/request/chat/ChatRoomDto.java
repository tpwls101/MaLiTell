package com.ssafy.malitell.dto.request.chat;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.ssafy.malitell.domain.chat.ChatRoom;
import com.ssafy.malitell.domain.user.User;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.io.Serializable;
import java.util.UUID;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@JsonInclude(JsonInclude.Include.NON_NULL)
public class ChatRoomDto implements Serializable {
    private static final String serialVersionUID = UUID.randomUUID().toString(); 
    private String chatRoomSeq;
    private int counselorSeq;
    private int clientSeq;
    private String counselorName;
    private String clientName;
    private String counselorProfileImg;
    private String clientProfileImg;

    public static ChatRoomDto create(User counselor, User client) {
        ChatRoomDto chatRoomDto = new ChatRoomDto();
        chatRoomDto.chatRoomSeq = serialVersionUID;
        chatRoomDto.counselorSeq = counselor.getUserSeq();
        chatRoomDto.clientSeq = client.getUserSeq();
        chatRoomDto.counselorName = counselor.getName();
        chatRoomDto.clientName = client.getName();
        chatRoomDto.counselorProfileImg = counselor.getProfileImg();
        chatRoomDto.clientProfileImg = client.getProfileImg();
        return chatRoomDto;
    }

    public ChatRoomDto(ChatRoom chatRoom) {
        this.chatRoomSeq = chatRoom.getChatRoomSeq();
        this.counselorSeq = chatRoom.getCounselor().getUserSeq();
        this.clientSeq = chatRoom.getClient().getUserSeq();
        this.counselorName = chatRoom.getCounselor().getName();
        this.clientName = chatRoom.getClient().getName();
        this.counselorProfileImg = chatRoom.getCounselor().getProfileImg();
        this.clientProfileImg = chatRoom.getClient().getProfileImg();
    }
}
