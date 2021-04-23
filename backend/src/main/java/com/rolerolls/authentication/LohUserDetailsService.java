package com.rolerolls.authentication;

import com.rolerolls.domain.contexts.Player;
import com.rolerolls.domain.contexts.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.ArrayList;

@Service
public class LohUserDetailsService implements UserDetailsService {

    @Autowired
    public UserRepository userRepository;
    @Autowired
    public PlayerRepository playerRepository;
    @Override
    public LohUserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        User user = userRepository.findByEmail(username);
        if (user != null) {
            Player player = playerRepository.findById(user.getId()).get();
            return new LohUserDetails(user.getUserName(), user.getEmail(), user.getPassword(), user.getId(), new ArrayList<>());
        } else {
            throw new UsernameNotFoundException("User not found with username: " + username);
        }
    }
}