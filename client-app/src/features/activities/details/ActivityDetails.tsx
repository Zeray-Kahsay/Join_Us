import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import { observer } from "mobx-react-lite";
import ActivityDetailedHeader from "./ActivityDetailedHeader";
import ActivityDetailedInfo from "./ActivityDetailedInfo";
import ActivityDetailedChat from "./ActivityDetailedChat";
import ActivityDetailedSideBar from "./ActivityDetailedSideBar";

const ActivityDetails = () => {
  const { activityStore } = useStore();
  const { selectedActivity, loadActivity, laodingInital } = activityStore;
  const { id } = useParams();

  useEffect(() => {
    if (id) loadActivity(id);
  }, [id, loadActivity]);

  if (laodingInital || !selectedActivity) return;

  return (
    <Grid>
      <Grid.Column width={10}>
        <ActivityDetailedHeader activity={selectedActivity} />
        <ActivityDetailedInfo activity={selectedActivity} />
        <ActivityDetailedChat />
      </Grid.Column>
      <Grid.Column width={6}>
        <ActivityDetailedSideBar activity={selectedActivity} />
      </Grid.Column>
    </Grid>
  );
};

export default observer(ActivityDetails);
