package com.loh.authentication;


import lombok.Getter;
import lombok.Setter;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.User;

import java.util.Collection;

public class LohUserDetails extends User {
    @Getter
    @Setter
    private String userName;
    public LohUserDetails(String userName, String email, String password, Collection<? extends GrantedAuthority> authorities) {
        super(email, password, authorities);
        this.userName = userName;
    }
}
