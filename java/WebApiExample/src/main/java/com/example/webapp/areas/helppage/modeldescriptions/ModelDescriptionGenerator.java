package com.example.webapp.areas.helppage.modeldescriptions;

import java.lang.annotation.Annotation;
import java.lang.reflect.Field;
import java.lang.reflect.Member;
import java.lang.reflect.Method;
import java.util.*;

public class ModelDescriptionGenerator {

    private final Map<Class<?>, Function<Object, String>> annotationTextGenerator = new HashMap<>();
    private final Map<Class<?>, String> defaultTypeDocumentation = new HashMap<>();
    private final Map<String, ModelDescription> generatedModels = new HashMap<>();

    public ModelDescriptionGenerator() {
        annotationTextGenerator.put(Required.class, a -> "Required");
        annotationTextGenerator.put(Range.class, a -> {
            Range range = (Range) a;
            return String.format("Range: inclusive between %d and %d", range.min(), range.max());
        });
        annotationTextGenerator.put(MaxLength.class, a -> {
            MaxLength maxLength = (MaxLength) a;
            return String.format("Max length: %d", maxLength.value());
        });
        annotationTextGenerator.put(MinLength.class, a -> {
            MinLength minLength = (MinLength) a;
            return String.format("Min length: %d", minLength.value());
        });
        annotationTextGenerator.put(StringLength.class, a -> {
            StringLength strLength = (StringLength) a;
            return String.format("String length: inclusive between %d and %d", strLength.min(), strLength.max());
        });
        annotationTextGenerator.put(DataType.class, a -> {
            DataType dataType = (DataType) a;
            return String.format("Data type: %s", dataType.value());
        });
        annotationTextGenerator.put(Pattern.class, a -> {
            Pattern pattern = (Pattern) a;
            return String.format("Matching regular expression pattern: %s", pattern.value());
        });

        defaultTypeDocumentation.put(Integer.class, "integer");
        defaultTypeDocumentation.put(Long.class, "integer");
        defaultTypeDocumentation.put(Short.class, "integer");
        defaultTypeDocumentation.put(Byte.class, "byte");
        defaultTypeDocumentation.put(Character.class, "character");
        defaultTypeDocumentation.put(Float.class, "decimal number");
        defaultTypeDocumentation.put(Double.class, "decimal number");
        defaultTypeDocumentation.put(String.class, "string");
        defaultTypeDocumentation.put(Boolean.class, "boolean");
        defaultTypeDocumentation.put(Date.class, "date");
    }

    public ModelDescription getOrCreateModelDescription(Class<?> modelType) {
        if (modelType == null) {
            throw new IllegalArgumentException("modelType cannot be null");
        }

        ModelDescription modelDescription = generatedModels.get(modelType.getName());
        if (modelDescription != null) {
            return modelDescription;
        }

        if (defaultTypeDocumentation.containsKey(modelType)) {
            return generateSimpleTypeModelDescription(modelType);
        }

        if (modelType.isEnum()) {
            return generateEnumTypeModelDescription(modelType);
        }

        if (Collection.class.isAssignableFrom(modelType)) {
            return generateCollectionModelDescription(modelType, Object.class);
        }

        if (Map.class.isAssignableFrom(modelType)) {
            return generateDictionaryModelDescription(modelType, Object.class, Object.class);
        }

        return generateComplexTypeModelDescription(modelType);
    }

    private ModelDescription generateSimpleTypeModelDescription(Class<?> modelType) {
        SimpleTypeModelDescription simpleModelDescription = new SimpleTypeModelDescription();
        simpleModelDescription.setName(modelType.getName());
        simpleModelDescription.setModelType(modelType);
        simpleModelDescription.setDocumentation(defaultTypeDocumentation.get(modelType));
        generatedModels.put(simpleModelDescription.getName(), simpleModelDescription);
        return simpleModelDescription;
    }

    private ModelDescription generateEnumTypeModelDescription(Class<?> modelType) {
        EnumTypeModelDescription enumDescription = new EnumTypeModelDescription();
        enumDescription.setName(modelType.getName());
        enumDescription.setModelType(modelType);
        enumDescription.setDocumentation(defaultTypeDocumentation.get(modelType));
        generatedModels.put(enumDescription.getName(), enumDescription);
        return enumDescription;
    }

    private ModelDescription generateCollectionModelDescription(Class<?> modelType, Class<?> elementType) {
        ModelDescription elementDescription = getOrCreateModelDescription(elementType);
        CollectionModelDescription collectionModelDescription = new CollectionModelDescription();
        collectionModelDescription.setName(modelType.getName());
        collectionModelDescription.setModelType(modelType);
        collectionModelDescription.setElementDescription(elementDescription);
        return collectionModelDescription;
    }

    private ModelDescription generateDictionaryModelDescription(Class<?> modelType, Class<?> keyType, Class<?> valueType) {
        ModelDescription keyModelDescription = getOrCreateModelDescription(keyType);
        ModelDescription valueModelDescription = getOrCreateModelDescription(valueType);
        DictionaryModelDescription dictionaryModelDescription = new DictionaryModelDescription();
        dictionaryModelDescription.setName(modelType.getName());
        dictionaryModelDescription.setModelType(modelType);
        dictionaryModelDescription.setKeyModelDescription(keyModelDescription);
        dictionaryModelDescription.setValueModelDescription(valueModelDescription);
        return dictionaryModelDescription;
    }

    private ModelDescription generateComplexTypeModelDescription(Class<?> modelType) {
        ComplexTypeModelDescription complexModelDescription = new ComplexTypeModelDescription();
        complexModelDescription.setName(modelType.getName());
        complexModelDescription.setModelType(modelType);
        complexModelDescription.setDocumentation(defaultTypeDocumentation.get(modelType));
        generatedModels.put(complexModelDescription.getName(), complexModelDescription);

        for (Field field : modelType.getDeclaredFields()) {
            ParameterDescription propertyModel = new ParameterDescription();
            propertyModel.setName(field.getName());
            propertyModel.setTypeDescription(getOrCreateModelDescription(field.getType()));
            complexModelDescription.getProperties().add(propertyModel);
        }

        for (Method method : modelType.getDeclaredMethods()) {
            ParameterDescription propertyModel = new ParameterDescription();
            propertyModel.setName(method.getName());
            propertyModel.setTypeDescription(getOrCreateModelDescription(method.getReturnType()));
            complexModelDescription.getProperties().add(propertyModel);
        }

        return complexModelDescription;
    }
}