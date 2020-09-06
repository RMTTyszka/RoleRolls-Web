package com.loh.authentication;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationCredentialsNotFoundException;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.crypto.bcrypt.BCrypt;

public class LohAuthenticationManager implements AuthenticationManager {
    @Autowired
    UserRepository userRepository;
    @Override
    public Authentication authenticate(Authentication authentication) throws AuthenticationException {
        User user = userRepository.findByEmail(authentication.getName());
        String password = authentication.getCredentials().toString();
        if (BCrypt.checkpw(password, user.getPassword())) {
            LohUserDetails userDetails = new LohUserDetails(user);
            return new LohUserAuthenticationToken(userDetails, user.getPassword());
        } else {
            throw new AuthenticationCredentialsNotFoundException("Invalid login or password, maybe both, who knows");
        }
    }
}
