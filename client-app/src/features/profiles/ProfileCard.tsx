import { observer } from "mobx-react-lite";
import { Profile } from "../../app/interfaces/profile";
import { Card, Icon, Image } from "semantic-ui-react";
import { Link } from "react-router-dom";

interface Props {
  profile: Profile;
}
const ProfileCard = ({ profile }: Props) => {
  return (
    <Card as={Link} to={`/profiles/${profile.username}`}>
      <Image src={profile.image || "/assets/user.png"} />
      <Card.Content>
        <Card.Header> {profile.displayName} </Card.Header>
        <Card.Header> Bio goes here </Card.Header>
      </Card.Content>
      <Card.Content extra>
        <Icon name="user" />
        100 followers
      </Card.Content>
    </Card>
  );
};

export default observer(ProfileCard);
