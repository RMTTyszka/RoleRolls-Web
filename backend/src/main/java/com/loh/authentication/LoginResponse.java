package com.loh.authentication;

import lombok.Getter;
import lombok.Setter;

public class LoginResponse {
    @Getter
    @Setter
    private String token;
    @Getter
    @Setter
    private String userName;

    public LoginResponse(String token, String userName) {
        this.token = token;
        this.userName = userName;
    }
}
