import './App.css';
import HeaderAppBar from './Components/HeaderAppBar';
import SocietyGrid from './Components/SocietyGrid';
import { useState } from 'react';


function App() {
    const [isSocietyDetails, setIsSocietyDetails] = useState(false);
    const [isManager, setIsManager] = useState(false);
    const [homeKey, setHomeKey] = useState(0);

    const handleLogoClick = () => {
        setIsSocietyDetails(false);
        setHomeKey((currentKey) => currentKey + 1);
    };
    
    return (
        <>
            <HeaderAppBar
                hideSearch={isSocietyDetails}
                isManager={isManager}
                onManagerLogin={() => setIsManager(true)}
                onLogout={() => setIsManager(false)}
                onLogoClick={handleLogoClick}
            />
            <SocietyGrid
                key={homeKey}
                onDetailsViewChange={setIsSocietyDetails}
                isManager={isManager}
            />
        </>
    );

}

export default App;