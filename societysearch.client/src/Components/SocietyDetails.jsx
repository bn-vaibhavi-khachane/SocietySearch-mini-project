import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Chip from '@mui/material/Chip';
import Divider from '@mui/material/Divider';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';

import PoolIcon from '@mui/icons-material/Pool';
import FitnessCenterIcon from '@mui/icons-material/FitnessCenter';
import NightlifeIcon from '@mui/icons-material/Nightlife';
import ChildFriendlyIcon from '@mui/icons-material/ChildFriendly';
import VideocamIcon from '@mui/icons-material/Videocam';
import LocalParkingIcon from '@mui/icons-material/LocalParking';
import LocalLibraryIcon from '@mui/icons-material/LocalLibrary';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

import LocationOnIcon from '@mui/icons-material/LocationOn';
import PersonIcon from '@mui/icons-material/Person';
import EmailIcon from '@mui/icons-material/Email';
import PhoneIcon from '@mui/icons-material/Phone';
import ApartmentIcon from '@mui/icons-material/Apartment';
import EventIcon from '@mui/icons-material/Event';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

const amenityIcons = {
    'Swimming Pool': PoolIcon,
    Gym: FitnessCenterIcon,
    Clubhouse: NightlifeIcon,
    "Children's Play Area": ChildFriendlyIcon,
    CCTV: VideocamIcon,
    Parking: LocalParkingIcon,
    Library: LocalLibraryIcon,
    Turf: SportsSoccerIcon,
};

const placeholderImage = 'https://placehold.co/1200x420?text=Society+Image';

function InfoRow({ icon: Icon, label, value }) {
    return (
        <Stack direction="row" spacing={1.5} alignItems="flex-start">
            <Icon color="primary" />
            <Box sx={{ textAlign: 'left' }}>
                <Typography variant="caption" color="text.secondary">
                    {label}
                </Typography>
                <Typography variant="body1">{value || '-'}</Typography>
            </Box>
        </Stack>
    );
}

export default function SocietyDetails({ society, onBack }) {
    const details = society ?? {};
    const amenities = details.amenities ?? [];
    const unitTypes = details.availableUnitTypes ?? [];

    return (
        <Box sx={{ maxWidth: 1200, mx: 'auto', mt: '90px', px: 2, pb: 6 }}>
            {onBack && (
                <Box sx={{ textAlign: 'left', mb: 2 }}>
                    <Button startIcon={<ArrowBackIcon />} onClick={onBack}>
                        Back to Societies
                    </Button>
                </Box>
            )}

            {/* Section 1: society image */}
            <Card sx={{ overflow: 'hidden' }}>
                <Box
                    component="img"
                    src={details.imageUrl || details.img || placeholderImage}
                    alt={details.name || details.title || 'Society'}
                    sx={{ width: '100%', height: { xs: 220, md: 420 }, objectFit: 'cover', display: 'block' }}
                />
                <CardContent sx={{ textAlign: 'left' }}>
                    <Typography variant="h4" fontWeight="bold">
                        {details.name || details.title || 'Society Details'}
                    </Typography>
                    <Stack direction="row" spacing={1} alignItems="center" sx={{ mt: 1 }}>
                        <LocationOnIcon fontSize="small" color="action" />
                        <Typography variant="body2" color="text.secondary">
                            {details.address || 'Address not available'}
                        </Typography>
                    </Stack>
                </CardContent>
            </Card>

            {/* Section 2: society information */}
            <Card sx={{ mt: 3 }}>
                <CardContent sx={{ textAlign: 'left' }}>
                    <Typography variant="h6" fontWeight="bold" gutterBottom>
                        Society Information
                    </Typography>
                    <Divider sx={{ mb: 2 }} />
                    {details.summary && (
                        <Typography variant="body1" sx={{ mb: 3 }}>
                            {details.summary}
                        </Typography>
                    )}
                    <Grid container spacing={3}>
                        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                            <InfoRow icon={PersonIcon} label="Manager" value={details.managerName} />
                        </Grid>
                        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                            <InfoRow icon={EmailIcon} label="Manager Email" value={details.managerEmail} />
                        </Grid>
                        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                            <InfoRow icon={PhoneIcon} label="Contact Number" value={details.managerContactNumber} />
                        </Grid>
                        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                            <InfoRow
                                icon={ApartmentIcon}
                                label="Units"
                                value={
                                    details.totalUnits
                                        ? `${details.availableUnits ?? 0} available of ${details.totalUnits}`
                                        : details.availableUnits
                                }
                            />
                        </Grid>
                        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                            <InfoRow icon={EventIcon} label="Established" value={details.establishmentYear} />
                        </Grid>
                        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
                            <Box sx={{ textAlign: 'left' }}>
                                <Typography variant="caption" color="text.secondary">
                                    Available Unit Types
                                </Typography>
                                <Stack direction="row" spacing={1} flexWrap="wrap" useFlexGap sx={{ mt: 0.5 }}>
                                    {unitTypes.length > 0 ? (
                                        unitTypes.map((unitType) => (
                                            <Chip
                                                key={unitType}
                                                label={unitType}
                                                size="small"
                                                color="primary"
                                                variant="outlined"
                                            />
                                        ))
                                    ) : (
                                        <Typography variant="body1">-</Typography>
                                    )}
                                </Stack>
                            </Box>
                        </Grid>
                    </Grid>
                </CardContent>
            </Card>

            {/* Section 3: amenities */}
            <Card sx={{ mt: 3 }}>
                <CardContent sx={{ textAlign: 'left' }}>
                    <Typography variant="h6" fontWeight="bold" gutterBottom>
                        Available Amenities
                    </Typography>
                    <Divider sx={{ mb: 2 }} />
                    {amenities.length === 0 ? (
                        <Typography variant="body2" color="text.secondary">
                            No amenities listed for this society.
                        </Typography>
                    ) : (
                        <Grid container spacing={2}>
                            {amenities.map((amenity) => {
                                const AmenityIcon = amenityIcons[amenity] ?? CheckCircleIcon;
                                return (
                                    <Grid key={amenity} size={{ xs: 6, sm: 4, md: 3 }}>
                                        <Paper
                                            variant="outlined"
                                            sx={{
                                                py: 2,
                                                px: 1,
                                                height: '100%',
                                                display: 'flex',
                                                flexDirection: 'column',
                                                alignItems: 'center',
                                                gap: 1,
                                            }}
                                        >
                                            <AmenityIcon color="primary" sx={{ fontSize: 40 }} />
                                            <Typography variant="body2" align="center">
                                                {amenity}
                                            </Typography>
                                        </Paper>
                                    </Grid>
                                );
                            })}
                        </Grid>
                    )}
                </CardContent>
            </Card>
        </Box>
    );
}