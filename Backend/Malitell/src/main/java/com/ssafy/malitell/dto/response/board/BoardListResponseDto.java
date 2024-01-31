package com.ssafy.malitell.dto.response.board;

import com.ssafy.malitell.domain.board.Board;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.sql.Timestamp;
import java.time.LocalDateTime;
import java.util.Optional;

@NoArgsConstructor
@AllArgsConstructor
@Getter
public class BoardListResponseDto {
    // 제목
    private String title;

    // 작성자 닉네임
    private String username;
    private int hit;
    private Timestamp time;

    public BoardListResponseDto(Board board) {
        this.title = board.getTitle();
        this.username = board.getUser().getNickname();
        this.hit = board.getHit();
        this.time = board.getTime();
    }
}