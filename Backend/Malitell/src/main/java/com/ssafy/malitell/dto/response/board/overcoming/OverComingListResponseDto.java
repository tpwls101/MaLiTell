package com.ssafy.malitell.dto.response.board.overcoming;

import com.ssafy.malitell.domain.board.OverComing;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.sql.Timestamp;


@NoArgsConstructor
@AllArgsConstructor
@Getter
public class OverComingListResponseDto {
    // 제목
    private String title;

    // 작성자 닉네임
    private String username;
    private int hit;
    private Timestamp time;

    private int boardSeq;

    public OverComingListResponseDto(OverComing overComing) {
        this.title = overComing.getTitle();
        this.username = overComing.getUser().getName();
        this.hit = overComing.getHit();
        this.time = overComing.getTime();
        this.boardSeq = overComing.getOvercomingSeq();
    }
}
