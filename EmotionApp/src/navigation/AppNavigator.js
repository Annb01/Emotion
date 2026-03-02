import StartScreen from '../screens/StartScreen';
import PsychHome from '../screens/PsychHome';
import UserHome from '../screens/UserHome';
import UserProfile from '../screens/UserProfile';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

const Stack = createNativeStackNavigator();

export default function AppNavigator() {
  return (
    <Stack.Navigator initialRouteName="Start">
      <Stack.Screen name="Start" component={StartScreen} options={{ headerShown: false }} />
      <Stack.Screen name="PsychHome" component={PsychHome} options={{ headerShown: false }} />
      <Stack.Screen name="UserHome" component={UserHome} options={{ headerShown: false }} />
      <Stack.Screen name="UserProfile" component={UserProfile} options={{ headerShown: false }} />
    </Stack.Navigator>
  );
}
