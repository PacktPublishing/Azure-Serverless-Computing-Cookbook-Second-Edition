package com.packtpub;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class CosmosDocument {
    private String id;

    public String getId() {
        return id;
    }

    public CosmosDocument setId(String id) {
        this.id = id;
        return this;
    }
}