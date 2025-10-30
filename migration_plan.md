# Migration Plan: .net_to_java (.Net Framework 4.7.2 → Java 17)

## Overview

This migration plan outlines a comprehensive, step-by-step approach for transitioning the legacy .NET Framework 4.7.2 application to Java 17 using modern frameworks and best practices. The migration covers all layers: project setup, configuration, dependency management, controllers, services, views, asset bundling, and testing. The plan is designed for incremental execution, minimizing risk and ensuring feature parity at each stage. Estimated timeline: 8–12 weeks for a small team, with high-risk items addressed early and thorough validation at each milestone.

---

## Migration Tasks

### Task 0: Project Foundation & Java Environment Setup

**Source Files:**
- WebAppDI-main/WebAppDI.sln
- WebAppDI-main/WebAppDI/WebAppDI.csproj
- WebAppDI-main/WebAppDILib/WebAppDILib.csproj
- WebAppDI-main/WebApp.Tests/WebApp.Tests.csproj

**Target Files:**
- java-app/pom.xml (or build.gradle)
- java-app/README.md
- java-app/.gitignore

**Instructions:**
1. Create a new Java 17 project structure using Maven (preferred) or Gradle.
2. Initialize `pom.xml` with Spring Boot, Thymeleaf, JUnit, and other required dependencies.
3. Set up `.gitignore` for Java, Maven, and IDE artifacts.
4. Document project structure and migration approach in `README.md`.
5. Ensure Java 17 is installed and configured in the build environment.

**Dependencies:** None

---

### Task 1: Dependency Mapping & Management

**Source Files:**
- WebAppDI-main/WebAppDI/WebAppDI.csproj
- WebAppDI-main/WebAppDILib/WebAppDILib.csproj
- WebAppDI-main/WebApp.Tests/WebApp.Tests.csproj

**Target Files:**
- java-app/pom.xml

**Instructions:**
1. Audit all NuGet package references in `.csproj` files.
2. Map each NuGet package to its Maven/Gradle equivalent (see Library and API Mappings).
3. Add mapped dependencies to `pom.xml`.
4. Remove .NET-specific build targets and ensure all required Java dependencies are present.

**Dependencies:** Task 0

---

### Task 2: Configuration Migration

**Source Files:**
- WebAppDI-main/WebAppDI/Web.config
- WebAppDI-main/WebAppDI/Web.Debug.config
- WebAppDI-main/WebAppDI/Web.Release.config
- WebAppDI-main/WebAppDI/Views/Web.config
- WebAppDI-main/config.json

**Target Files:**
- java-app/src/main/resources/application.properties
- java-app/src/main/resources/application.yml

**Instructions:**
1. Extract all settings from `.config` files and map to Spring Boot properties format.
2. Migrate environment-specific settings to Spring profiles (`application-dev.yml`, `application-prod.yml`).
3. Document all configuration mappings for future reference.
4. Remove deprecated or unused settings.

**Dependencies:** Task 1

---

### Task 3: Data Model & ViewModel Migration

**Source Files:**
- WebAppDI-main/WebAppDILib/ViewModels/IndexViewModel.cs

**Target Files:**
- java-app/src/main/java/com/example/viewmodel/IndexViewModel.java

**Instructions:**
1. Convert C# ViewModel classes to Java POJOs.
2. Replace C# properties with Java getters/setters.
3. Update data types as needed (e.g., `List<int>` → `List<Integer>`).
4. Ensure serialization compatibility for REST endpoints.

**Dependencies:** Task 2

---

### Task 4: Service Layer Migration

**Source Files:**
- WebAppDI-main/WebAppDILib/Services/DITestService.cs

**Target Files:**
- java-app/src/main/java/com/example/service/DITestService.java
- java-app/src/main/java/com/example/service/DITestServiceImpl.java

**Instructions:**
1. Convert service interfaces and implementations to Java.
2. Annotate service classes with `@Service` for Spring DI.
3. Refactor methods to Java conventions and update data types.
4. Remove .NET-specific DI registration logic.

**Dependencies:** Task 3

---

### Task 5: Dependency Injection & Startup Logic

**Source Files:**
- WebAppDI-main/WebAppDI/Startup.cs
- WebAppDI-main/WebAppDILib/Extensions/ServiceProviderExtensions.cs

**Target Files:**
- java-app/src/main/java/com/example/Application.java
- java-app/src/main/java/com/example/config/AppConfig.java

**Instructions:**
1. Implement Spring Boot main class (`Application.java`) with `@SpringBootApplication`.
2. Move DI setup to annotated configuration classes (`@Configuration`, `@Bean`).
3. Remove custom service provider extensions; rely on Spring Boot DI.
4. Document DI mappings and service lifetimes.

**Dependencies:** Task 4

---

### Task 6: Controllers Migration (MVC & REST)

**Source Files:**
- WebAppDI-main/WebAppDI/Controllers/HomeController.cs
- WebAppDI-main/WebAppDI/Controllers/ValuesController.cs
- WebAppDI-main/WebAppDILib/Controllers/AboutController.cs
- WebAppDI-main/WebAppDILib/Controllers/StringsController.cs

**Target Files:**
- java-app/src/main/java/com/example/controller/HomeController.java
- java-app/src/main/java/com/example/controller/ValuesController.java
- java-app/src/main/java/com/example/controller/AboutController.java
- java-app/src/main/java/com/example/controller/StringsController.java

**Instructions:**
1. Rewrite each controller as a Java class, using `@Controller` or `@RestController`.
2. Map C# attributes to Java annotations (`[HttpGet]` → `@GetMapping`, `[RoutePrefix]` → `@RequestMapping`).
3. Refactor constructor injection to use `@Autowired`.
4. Update action methods to return view names or POJOs as appropriate.
5. Replace .NET logging with SLF4J/Logback.

**Dependencies:** Task 5

---

### Task 7: Routing & App_Start Logic Migration

**Source Files:**
- WebAppDI-main/WebAppDI/App_Start/RouteConfig.cs
- WebAppDI-main/WebAppDI/App_Start/WebApiConfig.cs
- WebAppDI-main/WebAppDI/App_Start/FilterConfig.cs

**Target Files:**
- java-app/src/main/java/com/example/config/WebMvcConfig.java
- java-app/src/main/java/com/example/config/WebApiConfig.java
- java-app/src/main/java/com/example/config/GlobalExceptionHandler.java

**Instructions:**
1. Move routing logic to annotation-based mappings in controllers.
2. Implement global exception handling using `@ControllerAdvice` and `@ExceptionHandler`.
3. Document all route mappings and ensure URL parity.
4. Remove App_Start files from migration target.

**Dependencies:** Task 6

---

### Task 8: View Layer Migration (Razor to Thymeleaf/JSP)

**Source Files:**
- WebAppDI-main/WebAppDI/Views/Home/Index.cshtml
- WebAppDI-main/WebAppDI/Views/About/Index.cshtml
- WebAppDI-main/WebAppDI/Views/Shared/_Layout.cshtml
- WebAppDI-main/WebAppDI/Views/Shared/Error.cshtml
- WebAppDI-main/WebAppDI/Views/_ViewStart.cshtml

**Target Files:**
- java-app/src/main/resources/templates/home/index.html
- java-app/src/main/resources/templates/about/index.html
- java-app/src/main/resources/templates/shared/layout.html
- java-app/src/main/resources/templates/shared/error.html

**Instructions:**
1. Convert Razor views to Thymeleaf or JSP templates.
2. Map model bindings and layout includes to Thymeleaf syntax.
3. Refactor partials and shared layouts for Thymeleaf.
4. Validate UI rendering and model data integration.

**Dependencies:** Task 6

---

### Task 9: Asset Bundling & Frontend Optimization

**Source Files:**
- WebAppDI-main/WebAppDI/App_Start/BundleConfig.cs
- WebAppDI-main/WebAppDI/Content/
- WebAppDI-main/WebAppDI/Scripts/

**Target Files:**
- java-app/src/main/resources/static/css/
- java-app/src/main/resources/static/js/
- java-app/webpack.config.js (if using Webpack)

**Instructions:**
1. Move CSS and JS assets to Spring Boot static resources directory.
2. Set up Webpack or Maven plugins for asset bundling and minification.
3. Update Thymeleaf templates to reference new asset paths.
4. Remove server-side bundling logic; rely on frontend build tools.

**Dependencies:** Task 8

---

### Task 10: Global.asax & Application Lifecycle Migration

**Source Files:**
- WebAppDI-main/WebAppDI/Global.asax
- WebAppDI-main/WebAppDI/Global.asax.cs

**Target Files:**
- java-app/src/main/java/com/example/Application.java

**Instructions:**
1. Implement application lifecycle hooks in Spring Boot main class.
2. Move any startup/shutdown logic to appropriate Spring Boot lifecycle events.
3. Remove Global.asax files from migration target.

**Dependencies:** Task 5

---

### Task 11: Testing Framework Migration

**Source Files:**
- WebAppDI-main/WebApp.Tests/UnitTest1.cs
- WebAppDI-main/WebApp.Tests/WebApp.Tests.csproj

**Target Files:**
- java-app/src/test/java/com/example/UnitTest1.java

**Instructions:**
1. Rewrite MSTest unit tests as JUnit 5 tests.
2. Map test assertions and setup/teardown logic to JUnit conventions.
3. Ensure coverage of all migrated business logic and controllers.
4. Integrate tests with Maven/Gradle build lifecycle.

**Dependencies:** Task 6

---

### Task 12: CI/CD Pipeline Migration

**Source Files:**
- WebAppDI-main/AzurePipelines/azure-pipeline.yml
- WebAppDI-main/AzurePipelines/templates/build.WebAppDI.yml
- WebAppDI-main/AzurePipelines/templates/variables.WebAppDI.yml
- WebAppDI-main/AzurePipelines/templates/variables.yml

**Target Files:**
- java-app/.github/workflows/ci.yml (if using GitHub Actions)
- java-app/azure-pipelines.yml (updated for Java/Maven)
- java-app/Jenkinsfile (if using Jenkins)

**Instructions:**
1. Update pipeline scripts to build, test, and package Java application.
2. Replace .NET build steps with Maven/Gradle commands.
3. Add steps for running JUnit tests and packaging artifacts.
4. Document pipeline changes and validate CI/CD integration.

**Dependencies:** Task 11

---

### Task 13: Documentation & Knowledge Transfer

**Source Files:**
- WebAppDI-main/README.md
- WebAppDI-main/assessment.md

**Target Files:**
- java-app/README.md
- java-app/docs/migration-notes.md

**Instructions:**
1. Update README with new build, run, and deployment instructions.
2. Document all migration mappings, architectural changes, and known issues.
3. Provide onboarding notes for future maintainers.

**Dependencies:** All previous tasks

---

### Task 14: Final Validation & Performance Tuning

**Source Files:**
- All migrated files

**Target Files:**
- All migrated files

**Instructions:**
1. Perform end-to-end testing of the migrated application.
2. Profile application performance and optimize as needed.
3. Validate configuration, routing, and UI parity.
4. Address any remaining issues or regressions.

**Dependencies:** All previous tasks

---

## Library and API Mappings

| Old Library/API                        | New Library/API                        | Notes                                                      |
|----------------------------------------|----------------------------------------|------------------------------------------------------------|
| Microsoft.AspNet.Mvc                   | org.springframework.boot:spring-boot-starter-web | Spring Boot MVC for controllers and routing        |
| Microsoft.AspNet.WebApi                | org.springframework.boot:spring-boot-starter-web | REST endpoints via @RestController                 |
| Microsoft.Extensions.DependencyInjection| org.springframework:spring-context     | Spring DI/IoC container                                   |
| MSTest                                 | org.junit.jupiter:junit-jupiter        | JUnit 5 for unit testing                                   |
| Razor (.cshtml)                        | org.thymeleaf:thymeleaf                | Thymeleaf templates for views                              |
| Web.config, app.config                 | application.properties/yml             | Spring Boot configuration files                            |
| OWIN                                   | Spring Boot main class                 | Application startup and lifecycle                          |
| System.Web.Mvc.Controller              | org.springframework.stereotype.Controller | Java controller annotation                             |
| System.Web.Http.ApiController          | org.springframework.web.bind.annotation.RestController | REST controller annotation                  |
| [RoutePrefix], [HttpGet], etc.         | @RequestMapping, @GetMapping           | Java annotations for routing                               |
| ILogger<T>                             | org.slf4j.Logger, LoggerFactory        | SLF4J/Logback logging                                      |
| BundleConfig.cs                        | Webpack, Maven frontend plugins        | Asset bundling/minification handled by frontend tools      |
| ServiceCollection/AddTransient         | @Service, @Component, @Bean            | Spring DI annotations                                      |
| ViewModel (C# class)                   | Java POJO                              | Standard Java class with getters/setters                   |
| List<int>, IEnumerable<string>         | List<Integer>, List<String>            | Java collections                                           |
| async/await                            | CompletableFuture, @Async              | Java concurrency utilities                                 |
| LINQ                                   | Streams API                            | Java 8+ streams for collection manipulation                |
| NuGet packages                         | Maven/Gradle dependencies              | Java build tool dependency management                      |
| Global.asax                            | Spring Boot main class                 | Application lifecycle management                           |
| HandleErrorAttribute                   | @ControllerAdvice, @ExceptionHandler   | Global exception handling in Spring Boot                   |

---

## Migration Sequence

1. **Foundation and setup tasks**
    - Task 0: Project Foundation & Java Environment Setup
    - Task 1: Dependency Mapping & Management
2. **Data model updates**
    - Task 3: Data Model & ViewModel Migration
3. **Data access layer changes**
    - Task 4: Service Layer Migration
4. **Business logic updates**
    - Task 5: Dependency Injection & Startup Logic
    - Task 6: Controllers Migration (MVC & REST)
    - Task 7: Routing & App_Start Logic Migration
5. **Presentation layer changes**
    - Task 8: View Layer Migration (Razor to Thymeleaf/JSP)
    - Task 9: Asset Bundling & Frontend Optimization
6. **Configuration and deployment**
    - Task 2: Configuration Migration
    - Task 10: Global.asax & Application Lifecycle Migration
    - Task 12: CI/CD Pipeline Migration
7. **Testing and validation**
    - Task 11: Testing Framework Migration
    - Task 13: Documentation & Knowledge Transfer
    - Task 14: Final Validation & Performance Tuning

---

**Note:** Each task is independently testable and mapped to specific source and target files. Follow the sequence for a smooth, low-risk migration. Document all changes and validate thoroughly at each phase.