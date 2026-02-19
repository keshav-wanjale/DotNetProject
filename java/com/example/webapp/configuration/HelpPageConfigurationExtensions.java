package com.example.webapp.configuration;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class HelpPageConfigurationExtensions {

    @Bean
    public ModelDescriptionGenerator modelDescriptionGenerator() {
        return new ModelDescriptionGenerator();
    }

    // Additional configuration methods can be added here
}