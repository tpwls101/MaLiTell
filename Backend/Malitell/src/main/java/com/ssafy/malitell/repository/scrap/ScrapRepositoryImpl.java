package com.ssafy.malitell.repository.scrap;

import com.ssafy.malitell.domain.board.Gathering;
import jakarta.persistence.EntityManager;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
@RequiredArgsConstructor
public class ScrapRepositoryImpl {
    private final EntityManager entityManager;
    public List<Gathering> findUserScrapList(String userId) {
        return entityManager.createQuery("SELECT s.gathering FROM Scrap s WHERE s.user.userId = :userId", Gathering.class)
                .setParameter("userId", userId)
                .getResultList();
    }}
