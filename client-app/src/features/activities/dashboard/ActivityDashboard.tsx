import { Grid } from "semantic-ui-react";
import ActivityList from "./ActivityList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useEffect } from "react";
import ActivityFilters from "./ActivityFilters";

const ActivityDashboard = () => {
  const { activityStore } = useStore();
  const { loadActivities, activityRegistry } = activityStore;

  useEffect(() => {
    if (activityRegistry.size <= 1) activityStore.loadActivities();
  }, [loadActivities, activityRegistry, activityStore]);

  if (activityStore.laodingInital)
    return <LoadingComponent content="Loading activities" />;

  return (
    <Grid>
      <Grid.Column width="10"></Grid.Column>
      <ActivityList />

      <Grid.Column width="6">
        <ActivityFilters />
      </Grid.Column>
    </Grid>
  );
};

export default observer(ActivityDashboard);
