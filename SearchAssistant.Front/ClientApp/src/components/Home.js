import React, { useState } from "react";

export function Home() {
  const [message, setMessage] = useState("");
  const [results, setResults] = useState([]);

  function buildRequest(ev) {
    const searchRequest = {
      query: ev.target["query"].value,
      term: ev.target["term"].value,
      spiders: []
    };

    [...ev.target].forEach(el => {
      if (el.type === "checkbox" && el.name.startsWith("sp")) {
        if (el.checked) {
          searchRequest.spiders.push(el.value);
        }
      }
    });

    console.log(searchRequest);
    return searchRequest;
  }

  async function handleFormSubmit(ev) {
    ev.preventDefault();
    ev.persist();
    setMessage("Searching...");
    setResults([]);
    const searchRequest = buildRequest(ev);
    const response = await fetch(process.env.REACT_APP_SERVER_URL, {
      method: "POST",
      body: JSON.stringify(searchRequest),
      headers: {
        "Content-Type": "application/json"
      }
    });

    console.log(response);
    const data = await response.json();
    setResults(data);
    setMessage("");
  }

  return (
    <form onSubmit={handleFormSubmit}>
      <h1>Search</h1>
      <div className="form-group">
        <label htmlFor="query">Query string</label>
        <input type="text" id="query" name="query" className="form-control" defaultValue="online title search" />
      </div>

      <div className="form-group">
        <label htmlFor="term">Search term</label>
        <input type="text" id="term" name="term" className="form-control" defaultValue="www.infotrack.com.au" />
      </div>

      <label htmlFor="spiders">Spiders</label>
      <div className="form-group">
        <div className="form-check-inline">
          <input type="checkbox" value="GoogleWebSpider" id="google1" name="sp1" className="form-check-input" />
          <label htmlFor="google1" className="form-check-label">
            Google Web
          </label>
        </div>

        <div className="form-check-inline">
          <input type="checkbox" value="BingWebSpider" id="bing1" name="sp2" className="form-check-input" />
          <label htmlFor="bing1" className="form-check-label">
            Bing Web
          </label>
        </div>

        <div className="form-check-inline">
          <input type="checkbox" value="GoogleFileSpider" id="google2" name="sp3" className="form-check-input" />
          <label htmlFor="google2" className="form-check-label">
            Google File
          </label>
        </div>

        <div className="form-check-inline">
          <input type="checkbox" value="BingFileSpider" id="bing2" name="sp4" className="form-check-input" />
          <label htmlFor="bing2" className="form-check-label">
            Bing File
          </label>
        </div>
      </div>

      <label htmlFor="spiders">Options</label>
      <div className="form-group">
        <div className="form-check-inline">
          <input type="checkbox" id="o1" className="form-check-input" disabled={true} checked={true}/>
          <label htmlFor="o1" className="form-check-label">
            Ignore ads
          </label>
        </div>

        <div className="form-check-inline">
          <input type="checkbox" id="o2" className="form-check-input" disabled={true} checked={true}/>
          <label htmlFor="o2" className="form-check-label">
            Ignore videos
          </label>
        </div>

        <div className="form-check-inline">
          <input type="checkbox" id="o3" className="form-check-input" disabled={true} checked={true}/>
          <label htmlFor="o3" className="form-check-label">
            Case insensitive
          </label>
        </div>
      </div>

      <div className="form-group">
        <button type="submit" className="btn btn-primary">
          Search
        </button>
      </div>

      <div className="card">
        <div className="card-header">Search Results</div>
        <div className="card-body">
          <blockquote className="blockquote mb-0">{message}</blockquote>
          <ul>
            {results.map(result => (
              <li>{result}</li>
            ))}
          </ul>
        </div>
      </div>
    </form>
  );
}
