package com.ssafy.malitell.domain.selfhelpgroup;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import com.ssafy.malitell.domain.user.User;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@NoArgsConstructor
@Getter
@Setter
public class SelfHelpGroupUser {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @ManyToOne
    @JsonBackReference(value = "userrrrrrrr")
    @JoinColumn(name = "user_seq")
    private User user;

    @ManyToOne
    @JsonBackReference(value = "selfhelpgrouppppppp")
    @JoinColumn(name = "selfHelpGroup_id")
    private SelfHelpGroup selfHelpGroup;

    public SelfHelpGroupUser(User user, SelfHelpGroup selfHelpGroup) {
        this.user = user;
        this.selfHelpGroup = selfHelpGroup;
    }
}