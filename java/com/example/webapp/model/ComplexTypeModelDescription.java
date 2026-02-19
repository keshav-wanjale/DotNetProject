package com.example.webapp.model;

import java.util.List;
import java.util.ArrayList;

public class ComplexTypeModelDescription extends ModelDescription {

    private List<ParameterDescription> properties;

    public ComplexTypeModelDescription() {
        properties = new ArrayList<>();
    }

    public List<ParameterDescription> getProperties() {
        return properties;
    }

    public void setProperties(List<ParameterDescription> properties) {
        this.properties = properties;
    }
}