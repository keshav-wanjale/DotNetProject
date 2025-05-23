package com.webapi.example.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.provisioning.InMemoryUserDetailsManager;
import org.springframework.security.web.SecurityFilterChain;

/**
 * Security settings analogous to UnityConfig and FilterConfig in .NET.
 */
@Configuration
@EnableWebSecurity
public class SecurityConfig {

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        http
            .csrf().disable() // disabling CSRF for simplicity, configure as needed
            .authorizeRequests()
                .antMatchers("/", "/index.html", "/api/public/**", "/swagger-ui/**").permitAll()
                .anyRequest().authenticated()
            .and()
            .httpBasic();
        
        return http.build();
    }

    @Bean
    public UserDetailsService userDetailsService() {
        // In-memory authentication for demo; replace with real user service
        UserDetails user = User.builder()
            .username("user")
            .password("{noop}password")
            .roles("USER")
            .build();
            
        return new InMemoryUserDetailsManager(user);
    }
}