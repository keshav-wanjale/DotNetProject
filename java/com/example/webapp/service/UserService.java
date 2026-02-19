package com.example.webapp.service;

import com.example.webapp.model.User;
import com.example.webapp.repository.IUnitOfWork;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

@Service
public class UserService implements IUserService {

    private final IUnitOfWork db;
    private final Map<String, String> bannedNames;

    @Autowired
    public UserService(IUnitOfWork db) {
        this.db = db;
        this.bannedNames = new HashMap<>();
        this.bannedNames.put("admin", "admin");
        this.bannedNames.put("sa", "sa");
    }

    @Override
    public User addUser(String name, int age) {
        if (bannedNames.containsKey(name)) {
            throw new IllegalArgumentException("The name " + name + " is not allowed");
        }

        User user = new User();
        user.setName(name);
        user.setAge(age);

        User newUser = db.add(user);
        db.commit();

        return newUser;
    }

    @Override
    public List<User> getAllUsers() {
        return db.getAll(User.class);
    }

    @Override
    public User getUser(UUID id) {
        return db.get(User.class, id);
    }

    @Override
    public void remove(UUID id) {
        User existingUser = db.get(User.class, id);
        if (existingUser != null) {
            db.remove(existingUser);
            db.commit();
        }
    }
}
