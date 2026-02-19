package com.example.webapp.areas.helppage.modeldescriptions;

import java.lang.reflect.Type;
import java.util.Arrays;
import java.util.stream.Collectors;

public class ModelNameHelper {

    public static String getModelName(Type type) {
        ModelName modelNameAnnotation = type.getAnnotation(ModelName.class);
        if (modelNameAnnotation != null && !modelNameAnnotation.value().isEmpty()) {
            return modelNameAnnotation.value();
        }

        String modelName = type.getTypeName();
        if (type instanceof Class<?> && ((Class<?>) type).isArray()) {
            modelName = ((Class<?>) type).getComponentType().getTypeName() + "[]";
        }

        if (type instanceof Class<?> && ((Class<?>) type).isEnum()) {
            modelName = "Enum:" + modelName;
        }

        if (type instanceof Class<?> && ((Class<?>) type).getTypeParameters().length > 0) {
            String genericTypeName = ((Class<?>) type).getSimpleName();
            String[] argumentTypeNames = Arrays.stream(((Class<?>) type).getTypeParameters())
                    .map(Type::getTypeName)
                    .toArray(String[]::new);
            modelName = String.format("%s<%s>", genericTypeName, String.join(", ", argumentTypeNames));
        }

        return modelName;
    }
}