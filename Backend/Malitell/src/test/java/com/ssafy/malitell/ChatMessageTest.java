package com.ssafy.malitell;

<<<<<<< HEAD
import com.ssafy.malitell.domain.chat.CounselingChatMessage;
=======
import com.ssafy.malitell.domain.chat.ChatMessage;
>>>>>>> bbed58794e6e89ac38a8de8630be08f63c68e277
import com.ssafy.malitell.repository.RedisRepository;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import static java.time.LocalTime.now;


@SpringBootTest
public class ChatMessageTest {
    @Autowired
    private RedisRepository redisRepository;

    @Test
    void test() {
        CounselingChatMessage chatMessage = new CounselingChatMessage(1L, 1, 1, "hello!", now(), true);
        redisRepository.save(chatMessage);
        redisRepository.findById(chatMessage.getChatMessageSeq());
        redisRepository.count();
        redisRepository.delete(chatMessage);
    }

}