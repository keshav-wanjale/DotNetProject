package com.example.webapp.model;

/**
 * Describes a type model.
 */
public abstract class ModelDescription {

    private String documentation;

    private Class<?> modelType;

    private String name;

    // Getters and Setters
    public String getDocumentation() {
        return documentation;
    }

    public void setDocumentation(String documentation) {
        this.documentation = documentation;
    }

    public Class<?> getModelType() {
        return modelType;
    }

    public void setModelType(Class<?> modelType) {
        this.modelType = modelType;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}