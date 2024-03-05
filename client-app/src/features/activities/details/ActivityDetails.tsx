import { Button, Card, Image } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { Link, useParams } from "react-router-dom";
import { useEffect } from "react";
import { observer } from "mobx-react-lite";

const ActivityDetails = () => {
  const { activityStore } = useStore();
  const { selectedActivity, loadActivity, laodingInital } = activityStore;
  const { id } = useParams();

  useEffect(() => {
    if (id) loadActivity(id);
  }, [id, loadActivity]);

  if (laodingInital || !selectedActivity) return;

  return (
    <Card fluid>
      <Image src={`/assets/categoryImages/${selectedActivity.category}.jpg`} />
      <Card.Content>
        <Card.Header> {selectedActivity.title} </Card.Header>
        <Card.Meta>
          <span>{selectedActivity.date} </span>
        </Card.Meta>
        <Card.Description> {selectedActivity.description} </Card.Description>
      </Card.Content>
      <Card.Content extra>
        <Button.Group width="2">
          <Button
            as={Link}
            to={`/manage/${selectedActivity.id}`}
            basic
            color="blue"
            content="Edit"
          />
          <Button
            as={Link}
            to={"/activities"}
            basic
            color="grey"
            content="Cancel"
          />
        </Button.Group>
      </Card.Content>
    </Card>
  );
};

export default observer(ActivityDetails);
