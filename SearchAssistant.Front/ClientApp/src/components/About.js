import React, { Component } from "react";

export class About extends Component {
  render() {
    return (
      <div>
        <h1>About</h1>

        <h6>Front end application for the InfoTrack Coding Challenge.</h6>
        <br/>

        <p>The CEO from InfoTrack is very interested in SEO and how this can improve Sales. Every morning he logs in to his favourite search engine and types in the same key words "online title search" and counts down to see where and how many times their company, https://www.infotrack.com.au sits on the list.</p>
        <p>
          To make this task less tedious the CEO reaches out to you to write a small web-based application for him that will automatically perform this operation and return the result to the screen. The application prompts for a string of keywords to search, and a URL to find in the search results. The input values are then processed to return a string of numbers for where the URL is found in the
          search engine’s results. For example, "1, 10, 33" or "0". The CEO is only interested if their URL appears in the first 50 results.
        </p>

        <div className="alert alert-primary" role="alert">
          &copy; Dmitri Shosh, 2020
        </div>

      </div>
    );
  }
}
