package com.example.webapp.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.ModelAndView;

@RestController
@RequestMapping("/help")
public class HelpController {

    private static final String ERROR_VIEW_NAME = "Error";

    @GetMapping("/index")
    public ModelAndView index() {
        ModelAndView modelAndView = new ModelAndView();
        modelAndView.setViewName("index");
        // Add additional attributes to the model if needed
        return modelAndView;
    }

    @GetMapping("/api")
    public ModelAndView api(String apiId) {
        ModelAndView modelAndView = new ModelAndView();
        if (apiId != null && !apiId.isEmpty()) {
            // Logic to get HelpPageApiModel based on apiId
            // If found, set the view name and add the model to the view
            modelAndView.setViewName("api");
        } else {
            modelAndView.setViewName(ERROR_VIEW_NAME);
        }
        return modelAndView;
    }

    @GetMapping("/resourceModel")
    public ModelAndView resourceModel(String modelName) {
        ModelAndView modelAndView = new ModelAndView();
        if (modelName != null && !modelName.isEmpty()) {
            // Logic to get ModelDescription based on modelName
            // If found, set the view name and add the model to the view
            modelAndView.setViewName("resourceModel");
        } else {
            modelAndView.setViewName(ERROR_VIEW_NAME);
        }
        return modelAndView;
    }
}