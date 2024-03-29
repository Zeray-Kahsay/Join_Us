import { createContext, useContext } from "react";
import ActivityStore from "./ActivityStore";
import CommonStore from "./commonStore";
import UserStore from "./userStore";
import ModalStore from "./modalStore";
import ProfileStore from "./ProfileStore";

interface Store {
  activityStore: ActivityStore;
  commonStore: CommonStore;
  userStore: UserStore;
  modalStore: ModalStore;
  profileStore: ProfileStore;
}

export const store: Store = {
  activityStore: new ActivityStore(),
  commonStore: new CommonStore(),
  userStore: new UserStore(),
  modalStore: new ModalStore(),
  profileStore: new ProfileStore(),
};

export const StoreContext = createContext(store);

// custom hook
export function useStore() {
  return useContext(StoreContext);
}
