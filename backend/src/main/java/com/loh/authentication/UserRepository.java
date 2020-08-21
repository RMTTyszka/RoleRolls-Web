package com.loh.authentication;

import com.loh.shared.BaseRepository;

public interface UserRepository extends BaseRepository<User> {
    User findByEmail(String email);
    boolean existsByEmailOrUserName(String email, String userName);
}
