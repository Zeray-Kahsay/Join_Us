import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../interfaces/serverError";

export default class CommonStore {
  error: ServerError | null = null;
  token: string | undefined | null = localStorage.getItem("jwt");
  appLoaded = false;

  constructor() {
    makeAutoObservable(this);

    // reacts to changes on token variable & doesn't run the first time the app runs
    reaction(
      () => this.token,
      (token) => {
        if (token) {
          localStorage.setItem("jwt", token);
        } else {
          localStorage.removeItem("jwt");
        }
      }
    );
  }

  setServerError(error: ServerError) {
    this.error = error;
  }

  setToken = (token: string | null) => {
    if (token) localStorage.setItem("jwt", token);
    this.token = token;
  };

  setAppLoaded = () => {
    this.appLoaded = true;
  };
}
