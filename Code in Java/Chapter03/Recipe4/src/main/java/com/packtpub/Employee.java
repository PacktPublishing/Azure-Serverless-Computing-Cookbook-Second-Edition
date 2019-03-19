package com.packtpub;

import java.util.UUID;

public class Employee {
    private String partitionKey = "employee";
    private String rowKey = UUID.randomUUID().toString();
    private String firstName = "";
    private String lastName = "";
    private String profilePictureUrl = "";
    private String emailAddress = "";
    private String mobileNumber = "";

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

    public String getProfilePictureUrl() {
        return profilePictureUrl;
    }

    public Employee setProfilePictureUrl(String profilePictureUrl) {
        if (profilePictureUrl != null) {
            this.profilePictureUrl = profilePictureUrl.trim();
        }
        else {
            this.profilePictureUrl = "";
        }
        return this;
    }

    public String getEmailAddress() {
        return emailAddress;
    }

    public Employee setEmailAddress(String emailAddress) {
        if (emailAddress != null) {
            this.emailAddress = emailAddress.trim();
        }
        else {
            this.emailAddress = "";
        }
        return this;
    }

    public String getMobileNumber() {
        return mobileNumber;
    }

    public Employee setMobileNumber(String mobileNumber) {
        if (mobileNumber != null) {
            this.mobileNumber = mobileNumber.trim();
        }
        else {
            this.mobileNumber = "";
        }
        return this;
    }}