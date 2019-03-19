package com.packtpub;

import java.util.UUID;

public class Employee {
    private String partitionKey = "employee";
    private String rowKey = UUID.randomUUID().toString();
    private String firstName = "";
    private String lastName = "";

    public String getPartitionKey() {
        return partitionKey;
    }

    public String getRowKey() {
        return rowKey;
    }

    public String getFirstName() {
        return firstName;
    }

    public Employee setFirstName(String firstName) {
        if (firstName != null) {
            this.firstName = firstName.trim();
        }
        else {
            this.firstName = "";
        }
        return this;
    }

    public String getLastName() {
        return lastName;
    }

    public Employee setLastName(String lastName) {
        if (lastName != null) {
            this.lastName = lastName.trim();
        }
        else {
            this.lastName = "";
        }
        return this;
    }
}