package com.loh.authentication;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.DisabledException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.bcrypt.BCrypt;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/users",  produces = "application/json; charset=UTF-8")
public class UserController {

    @Autowired
    private AuthenticationManager authenticationManager;
    @Autowired
    UserRepository userRepository;
    @Autowired
    private LohUserDetailsService userDetailsService;
    @Autowired
    private JwtTokenUtil jwtTokenUtil;

    @PostMapping(path="/login")
    public @ResponseBody
    LoginResponse login(@RequestBody LoginInput input) throws Exception {
        authenticate(input.getUsername(), input.getPassword());
        final UserDetails userDetails = userDetailsService
                .loadUserByUsername(input.getUsername());
        final String token = jwtTokenUtil.generateToken(userDetails);
        return new LoginResponse(token);
    }
    @PostMapping(path="/update")
    public @ResponseBody
    void update(@RequestBody User user) {
        User userToUpdate = userRepository.findById(user.getId()).get();
        userToUpdate.setEmail(user.getEmail());
        String salt = BCrypt.gensalt(10);
        String password = BCrypt.hashpw(user.getPassword(), salt);
        userToUpdate.setPassword(password);
        userToUpdate.setSalt(salt);
        userRepository.save(userToUpdate);
    }
    @PostMapping(path="/create")
    public @ResponseBody
    void create(@RequestBody User user) {
        String salt = BCrypt.gensalt(10);
        String password = BCrypt.hashpw(user.getPassword(), salt);
        user.setPassword(password);
        user.setSalt(salt);
        userRepository.save(user);
    }

    private void authenticate(String username, String password) throws Exception {
        try {
            authenticationManager.authenticate(new UsernamePasswordAuthenticationToken(username, password));
        } catch (DisabledException e) {
            throw new Exception("USER_DISABLED", e);
        } catch (BadCredentialsException e) {
            throw new Exception("INVALID_CREDENTIALS", e);
        }
    }


}
