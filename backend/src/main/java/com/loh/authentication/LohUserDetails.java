package com.loh.authentication;


import lombok.Getter;
import lombok.Setter;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.User;

import java.security.Principal;
import java.util.ArrayList;
import java.util.Collection;
import java.util.UUID;

public class LohUserDetails extends User {
    @Getter
    @Setter
    private String userName;
    @Getter
    @Setter
    private UUID userId;
    public LohUserDetails(String userName, String email, String password, UUID userId, Collection<? extends GrantedAuthority> authorities) {
        super(email, password, authorities);
        this.userName = userName;
        this.userId = userId;
    }

    public LohUserDetails(com.loh.authentication.User user) {
        super(user.getEmail(), user.getPassword(),  new ArrayList<>());
        this.userName = user.getUserName();
        this.userId = user.getId();
    }

    public static UUID userId(Principal principal) {
        LohUserDetails user = ((LohUserAuthenticationToken) principal).getPrincipal();
        return user.getUserId();
    }
}
