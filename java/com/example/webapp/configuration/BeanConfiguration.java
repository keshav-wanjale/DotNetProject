package com.example.webapp.configuration;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import com.example.webapp.service.UserService;
import com.example.webapp.service.IUserService;

@Configuration
public class BeanConfiguration {

    @Bean
    public IUserService userService() {
        return new UserService();
    }

    // Add more beans as necessary
}
