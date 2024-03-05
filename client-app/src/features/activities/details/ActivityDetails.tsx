import { Button, Card, Image } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

const ActivityDetails = () => {
  const { activityStore } = useStore();
  const { selectedActivity, openForm, cancelSelectedActivity } = activityStore;

  if (!selectedActivity) return;

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
            onClick={() => openForm(selectedActivity.id)}
            basic
            color="blue"
            content="Edit"
          />
          <Button
            onClick={cancelSelectedActivity}
            basic
            color="grey"
            content="Cancell"
          />
        </Button.Group>
      </Card.Content>
    </Card>
  );
};

export default ActivityDetails;
