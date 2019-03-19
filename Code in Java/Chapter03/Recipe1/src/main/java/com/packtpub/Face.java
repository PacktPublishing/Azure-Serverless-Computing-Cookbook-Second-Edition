package com.packtpub;

import java.util.UUID;

public class Face {
    private String partitionKey = "faces";
    private String rowKey = UUID.randomUUID().toString();
    private int left;
    private int top;
    private int width;
    private int height;
    private String image;

    public String getPartitionKey() {
        return partitionKey;
    }

    public String getRowKey() {
        return rowKey;
    }

    public int getLeft() {
        return left;
    }

    public Face setLeft(int left) {
        this.left = left;
        return this;
    }

    public int getTop() {
        return top;
    }

    public Face setTop(int top) {
        this.top = top;
        return this;
    }

    public int getWidth() {
        return width;
    }

    public Face setWidth(int width) {
        this.width = width;
        return this;
    }

    public int getHeight() {
        return height;
    }

    public Face setHeight(int height) {
        this.height = height;
        return this;
    }

    public String getImage() {
        return image;
    }

    public Face setImage(String image) {
        this.image = image;
        return this;
    }
}