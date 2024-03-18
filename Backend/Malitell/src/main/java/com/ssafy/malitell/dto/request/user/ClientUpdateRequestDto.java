package com.ssafy.malitell.dto.request.user;

import com.ssafy.malitell.domain.tag.StatusTag;
import com.ssafy.malitell.dto.request.tag.TagRequestDto;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@AllArgsConstructor
@NotNull
@Setter
public class ClientUpdateRequestDto {
    private String name;
    private String email;
    private String phone;
    private List<String> statusTags;
    private String profileImg;
}
