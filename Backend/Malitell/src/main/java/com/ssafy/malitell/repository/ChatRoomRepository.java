package com.ssafy.malitell.repository;

import com.ssafy.malitell.domain.User;
import com.ssafy.malitell.domain.chat.ChatRoom;
import com.ssafy.malitell.dto.request.ChatRequestDto;
import jakarta.persistence.EntityManager;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class ChatRoomRepository {
    @Autowired
    EntityManager em;

    @Transactional
    public void save(ChatRoom chatRoom) {
        em.persist(chatRoom);
    }

    // 최근 순으로 목록 조회
    public List<ChatRoom> findAllRoomByLastSpentTimeDesc() {
        return em.createQuery("SELECT cr FROM ChatRoom cr ORDER BY lastSpentTime DESC", ChatRoom.class)
                .getResultList();
    }

    public ChatRoom findRoomByChatRoomSeq(String chatRoomSeq) {
        return em.find(ChatRoom.class, chatRoomSeq);
    }

    public ChatRoom findRoomCounselorAndClient(int counselorSeq, int clientSeq) {
        return (ChatRoom) em.createQuery("SELECT cr FROM ChatRoom cr WHERE cr.counselor.userSeq = :counselor AND cr.client.userSeq = :client")
                .setParameter("counselor", counselorSeq)
                .setParameter("client", clientSeq)
                .getSingleResult();
    }

    public ChatRoom createChatRoom(User counselor, User client) {
        return ChatRoom.create(counselor, client);
    }
}