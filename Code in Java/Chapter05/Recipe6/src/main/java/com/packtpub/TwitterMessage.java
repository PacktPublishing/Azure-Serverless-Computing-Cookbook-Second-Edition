package com.packtpub;

import com.fasterxml.jackson.annotation.JsonProperty;

public class TwitterMessage {
    private int followersCount;
    @JsonProperty("tweettext")
    private String tweetText;
    @JsonProperty("Name")
    private String name;

    public int getFollowersCount() {
        return followersCount;
    }

    public TwitterMessage setFollowersCount(int followersCount) {
        this.followersCount = followersCount;
        return this;
    }

    public String getTweetText() {
        return tweetText;
    }

    public TwitterMessage setTweetText(String tweetText) {
        this.tweetText = tweetText;
        return this;
    }

    public String getName() {
        return name;
    }

    public TwitterMessage setName(String name) {
        this.name = name;
        return this;
    }



}