package com.rolerolls.authentication;

import lombok.Getter;
import lombok.Setter;

import java.util.UUID;

public class LoginResponse {
    @Getter
    @Setter
    private String token;
    @Getter
    @Setter
    private String userName;
    @Getter
    @Setter
    private UUID userId;

    public LoginResponse(String token, String userName, UUID userId) {
        this.token = token;
        this.userName = userName;
        this.userId = userId;
    }
}
