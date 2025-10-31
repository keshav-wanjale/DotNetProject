# Migration Plan: .net_to_java (.Net Framework 4.7.2 → Java 17)

## Overview

This migration plan details the step-by-step process for migrating the legacy .NET Framework 4.7.2 web application to a modern Java 17 stack using Spring Boot. The migration covers all application layers, including project setup, configuration, dependency management, controllers, services, views, assets, and testing. The plan is structured to ensure minimal disruption, clear task dependencies, and comprehensive coverage of all code and configuration changes. The estimated timeline is 8–12 weeks for a small team, with high-risk areas identified for special attention.

**Key Milestones:**
- Java project scaffolding and dependency mapping
- Data model and service migration
- Controller and routing migration
- View/template migration
- Configuration and asset migration
- Testing and validation
- CI/CD and deployment updates

---

## Migration Tasks

### Task 0: Initialize Java Project Structure & Version Control

**Source Files:**
- N/A (new project setup)

**Target Files:**
- src/main/java/com/example/ (Java package root)
- src/main/resources/
- pom.xml (for Maven) or build.gradle (for Gradle)
- .gitignore
- README.md

**Instructions:**
1. Create a new Java 17 project using Spring Initializr or manually with Maven/Gradle.
2. Set up the base package structure: `com.example`.
3. Add `.gitignore` for Java/Maven/Gradle.
4. Initialize version control (Git) and link to the existing repository.
5. Add a new `README.md` describing the Java stack and migration plan.

**Dependencies:** None

---

### Task 1: Map and Migrate Project Dependencies

**Source Files:**
- WebAppDI-main/WebAppDI/WebAppDI.csproj
- WebAppDI-main/WebAppDILib/WebAppDILib.csproj
- WebAppDI-main/WebApp.Tests/WebApp.Tests.csproj

**Target Files:**
- pom.xml or build.gradle

**Instructions:**
1. Audit all NuGet packages and .NET references in `.csproj` files.
2. Identify Java equivalents for each dependency (see Library and API Mappings).
3. Add dependencies to `pom.xml` or `build.gradle` (Spring Boot, Thymeleaf, SLF4J, JUnit, etc.).
4. Remove .NET-specific references.
5. Document any dependencies with no direct Java equivalent for manual rewrite.

**Dependencies:** Task 0

---

### Task 2: Migrate Configuration Files

**Source Files:**
- WebAppDI-main/WebAppDI/Web.config
- WebAppDI-main/WebAppDI/Web.Debug.config
- WebAppDI-main/WebAppDI/Web.Release.config
- WebAppDI-main/WebAppDI/Views/Web.config

**Target Files:**
- src/main/resources/application.properties
- src/main/resources/application-dev.properties
- src/main/resources/application-prod.properties

**Instructions:**
1. Extract all settings from `.config` files (appSettings, connectionStrings, etc.).
2. Map each setting to Spring Boot's `application.properties` format.
3. Use Spring profiles for environment-specific settings (dev/prod).
4. Document any settings with no Java equivalent for manual handling.
5. Remove XML-based configuration.

**Dependencies:** Task 1

---

### Task 3: Migrate Data Models and ViewModels

**Source Files:**
- WebAppDI-main/WebAppDILib/ViewModels/IndexViewModel.cs

**Target Files:**
- src/main/java/com/example/viewmodel/IndexViewModel.java

**Instructions:**
1. Convert C# classes to Java POJOs.
2. Replace properties with explicit getter/setter methods or use Lombok annotations.
3. Update data types as needed (e.g., `List<int>` to `List<Integer>`).
4. Remove .NET-specific attributes.

**Dependencies:** Task 1

---

### Task 4: Migrate Service Layer

**Source Files:**
- WebAppDI-main/WebAppDILib/Services/DITestService.cs
- WebAppDI-main/WebAppDILib/Services/IDITestService.cs (if exists)

**Target Files:**
- src/main/java/com/example/service/DITestService.java
- src/main/java/com/example/service/IDITestService.java

**Instructions:**
1. Convert service interfaces and implementations to Java.
2. Annotate service classes with `@Service`.
3. Refactor methods to use Java collections and types.
4. Remove .NET-specific DI code.

**Dependencies:** Task 3

---

### Task 5: Migrate Dependency Injection Setup

**Source Files:**
- WebAppDI-main/WebAppDI/Startup.cs
- WebAppDI-main/WebAppDI/App_Start/FilterConfig.cs
- WebAppDI-main/WebAppDI/App_Start/BundleConfig.cs

**Target Files:**
- src/main/java/com/example/Application.java
- src/main/java/com/example/config/FilterConfig.java

**Instructions:**
1. Create a Spring Boot main class (`@SpringBootApplication`).
2. Move DI registration to Spring annotations (`@Service`, `@Component`, `@Autowired`).
3. Implement global filters using `@ControllerAdvice` and `@ExceptionHandler`.
4. Document any custom middleware for manual migration.

**Dependencies:** Task 4

---

### Task 6: Migrate Controllers (WebAppDI)

**Source Files:**
- WebAppDI-main/WebAppDI/Controllers/HomeController.cs
- WebAppDI-main/WebAppDI/Controllers/ValuesController.cs

**Target Files:**
- src/main/java/com/example/controller/HomeController.java
- src/main/java/com/example/controller/ValuesController.java

**Instructions:**
1. Convert controllers to Java classes annotated with `@Controller` or `@RestController`.
2. Replace routing attributes with `@RequestMapping`, `@GetMapping`, etc.
3. Inject services using `@Autowired`.
4. Replace logging with SLF4J.
5. Refactor action methods to return view names or `ResponseEntity`.
6. Map model binding to Spring's `Model` or method parameters.

**Dependencies:** Task 5

---

### Task 7: Migrate Controllers (WebAppDILib)

**Source Files:**
- WebAppDI-main/WebAppDILib/Controllers/AboutController.cs
- WebAppDI-main/WebAppDILib/Controllers/StringsController.cs

**Target Files:**
- src/main/java/com/example/controller/AboutController.java
- src/main/java/com/example/controller/StringsController.java

**Instructions:**
1. Repeat the process from Task 6 for these controllers.
2. Ensure all endpoints are mapped and tested.

**Dependencies:** Task 6

---

### Task 8: Migrate Routing and Web API Configuration

**Source Files:**
- WebAppDI-main/WebAppDI/App_Start/RouteConfig.cs
- WebAppDI-main/WebAppDI/App_Start/WebApiConfig.cs
- WebAppDI-main/WebAppDI/Global.asax
- WebAppDI-main/WebAppDI/Global.asax.cs

**Target Files:**
- src/main/java/com/example/config/WebMvcConfig.java

**Instructions:**
1. Implement routing using Spring Boot's annotation-based approach.
2. Move any global initialization logic to `Application.java` or `@Configuration` classes.
3. Remove Global.asax and related startup logic.
4. Document any custom routing patterns for manual migration.

**Dependencies:** Task 6

---

### Task 9: Migrate Views (Razor to Thymeleaf)

**Source Files:**
- WebAppDI-main/WebAppDI/Views/Home/Index.cshtml
- WebAppDI-main/WebAppDI/Views/About/Index.cshtml
- WebAppDI-main/WebAppDI/Views/Shared/_Layout.cshtml
- WebAppDI-main/WebAppDI/Views/Shared/Error.cshtml
- WebAppDI-main/WebAppDI/Views/_ViewStart.cshtml

**Target Files:**
- src/main/resources/templates/home/index.html
- src/main/resources/templates/about/index.html
- src/main/resources/templates/layout.html
- src/main/resources/templates/error.html

**Instructions:**
1. Rewrite each `.cshtml` file as a Thymeleaf template (`.html`).
2. Replace Razor syntax (`@model`, `@{}`) with Thymeleaf expressions (`th:*`).
3. Update layout and partials to Thymeleaf includes/fragments.
4. Map model properties to Thymeleaf variables.
5. Test rendering for UI parity.

**Dependencies:** Task 6

---

### Task 10: Migrate Static Assets

**Source Files:**
- WebAppDI-main/WebAppDI/Content/*
- WebAppDI-main/WebAppDI/Scripts/*
- WebAppDI-main/WebAppDI/favicon.ico

**Target Files:**
- src/main/resources/static/css/*
- src/main/resources/static/js/*
- src/main/resources/static/favicon.ico

**Instructions:**
1. Copy CSS, JS, and image files to the appropriate static resource folders.
2. Update asset references in Thymeleaf templates.
3. Remove .NET bundling/minification; use Maven/Gradle plugins or Webpack if needed.

**Dependencies:** Task 9

---

### Task 11: Migrate and Refactor Extension Methods

**Source Files:**
- WebAppDI-main/WebAppDILib/Extensions/ServiceProviderExtensions.cs

**Target Files:**
- src/main/java/com/example/util/ServiceProviderExtensions.java

**Instructions:**
1. Convert extension methods to static utility methods in Java.
2. Refactor usages in service and controller classes.

**Dependencies:** Task 4

---

### Task 12: Migrate Unit and Integration Tests

**Source Files:**
- WebAppDI-main/WebApp.Tests/UnitTest1.cs
- WebAppDI-main/WebApp.Tests/WebApp.Tests.csproj

**Target Files:**
- src/test/java/com/example/UnitTest1.java

**Instructions:**
1. Rewrite MSTest tests as JUnit tests.
2. Update test assertions and setup/teardown methods.
3. Ensure coverage of all migrated business logic and controllers.
4. Add integration tests for key endpoints.

**Dependencies:** Task 6

---

### Task 13: Update CI/CD Pipeline

**Source Files:**
- WebAppDI-main/AzurePipelines/azure-pipeline.yml
- WebAppDI-main/AzurePipelines/templates/build.WebAppDI.yml
- WebAppDI-main/AzurePipelines/templates/variables.WebAppDI.yml
- WebAppDI-main/AzurePipelines/templates/variables.yml

**Target Files:**
- .github/workflows/build.yml (if using GitHub Actions)
- Jenkinsfile (if using Jenkins)
- Updated Azure DevOps pipeline YAMLs for Java

**Instructions:**
1. Update build pipeline to use Maven/Gradle build steps.
2. Add steps for running JUnit tests.
3. Update artifact packaging and deployment steps for Java.
4. Remove .NET-specific build tasks.

**Dependencies:** Task 1, Task 12

---

### Task 14: Documentation and Developer Onboarding

**Source Files:**
- WebAppDI-main/README.md

**Target Files:**
- README.md (updated)
- MIGRATION_NOTES.md

**Instructions:**
1. Update documentation to reflect the new Java stack, build, and run instructions.
2. Document key migration decisions, library mappings, and known issues.
3. Provide onboarding steps for developers.

**Dependencies:** All previous tasks

---

### Task 15: Testing, Validation, and Performance Optimization

**Source Files:**
- N/A (validation phase)

**Target Files:**
- N/A

**Instructions:**
1. Perform end-to-end testing of all migrated features.
2. Validate API contracts and UI rendering.
3. Profile application performance and optimize as needed.
4. Fix any migration bugs or regressions.

**Dependencies:** Task 12

---

## Library and API Mappings

| Old Library/API                             | New Library/API                         | Notes                                                                                   |
|---------------------------------------------|-----------------------------------------|-----------------------------------------------------------------------------------------|
| System.Web.Mvc.Controller                   | org.springframework.stereotype.Controller | Use `@Controller` annotation                                                            |
| System.Web.Http.ApiController               | org.springframework.web.bind.annotation.RestController | Use `@RestController` annotation                                            |
| [RoutePrefix], [HttpGet], [Route]           | @RequestMapping, @GetMapping, etc.      | Use Spring's annotation-based routing                                                   |
| Microsoft.Extensions.DependencyInjection    | org.springframework.beans.factory.annotation.Autowired, @Service | Use Spring DI annotations                                              |
| Microsoft.Extensions.Logging.ILogger<T>     | org.slf4j.Logger, org.slf4j.LoggerFactory | Use SLF4J/Logback for logging                                                           |
| MSTest                                     | JUnit                                  | Use JUnit 5 for unit and integration testing                                            |
| Razor (.cshtml)                            | Thymeleaf (.html)                      | Rewrite views in Thymeleaf syntax                                                       |
| Web.config, app.config                      | application.properties, application.yml | Use Spring Boot configuration files                                                     |
| OWIN Startup.cs, Global.asax                | @SpringBootApplication main class       | Use Spring Boot's application entry point                                               |
| BundleConfig, WebGrease                     | Maven/Gradle plugins, Webpack           | Use frontend build tools for asset management                                           |
| System.Data, Entity Framework               | JDBC, Spring Data JPA                   | If present, migrate to Java data access frameworks                                      |
| Properties (C#)                             | Getter/Setter methods or Lombok         | Use explicit Java methods or Lombok annotations                                         |
| LINQ                                        | Java Streams API                        | Use streams for collection operations                                                   |
| async/await                                 | CompletableFuture, @Async               | Use Java concurrency primitives                                                         |
| [HandleError], GlobalFilterCollection       | @ControllerAdvice, @ExceptionHandler    | Use Spring's global exception handling                                                  |
| NuGet packages                              | Maven/Gradle dependencies               | Map each NuGet package to a Java equivalent or rewrite as needed                        |

---

## Migration Sequence

1. **Foundation and Setup**
   - Task 0: Initialize Java Project Structure & Version Control
   - Task 1: Map and Migrate Project Dependencies

2. **Configuration**
   - Task 2: Migrate Configuration Files

3. **Data Models**
   - Task 3: Migrate Data Models and ViewModels

4. **Business Logic and Services**
   - Task 4: Migrate Service Layer
   - Task 5: Migrate Dependency Injection Setup
   - Task 11: Migrate and Refactor Extension Methods

5. **Controllers and Routing**
   - Task 6: Migrate Controllers (WebAppDI)
   - Task 7: Migrate Controllers (WebAppDILib)
   - Task 8: Migrate Routing and Web API Configuration

6. **Presentation Layer**
   - Task 9: Migrate Views (Razor to Thymeleaf)
   - Task 10: Migrate Static Assets

7. **Testing and Validation**
   - Task 12: Migrate Unit and Integration Tests
   - Task 15: Testing, Validation, and Performance Optimization

8. **CI/CD and Deployment**
   - Task 13: Update CI/CD Pipeline

9. **Documentation**
   - Task 14: Documentation and Developer Onboarding

---

**Note:**  
Each task should be completed and validated before proceeding to dependent tasks. High-risk areas (controllers, views, configuration) require thorough testing and review. Document all manual rewrites and decisions for future maintainability.