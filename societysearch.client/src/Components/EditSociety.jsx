import * as React from 'react';
import Button from '@mui/material/Button';
import Checkbox from '@mui/material/Checkbox';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import FormControl from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormGroup from '@mui/material/FormGroup';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';

const amenityOptions = [
	'Swimming Pool',
	'Gym',
	'Clubhouse',
	"Children's Play Area",
	'CCTV',
	'Parking',
	'Library',
	'Turf',
];

const unitTypeOptions = ['1 BHK', '2 BHK', '3 BHK', '4 BHK', 'Penthouse', 'Studio'];

const emptySociety = {
	name: '',
	imageUrl: '',
	address: '',
	summary: '',
	managerName: '',
	managerEmail: '',
	managerContactNumber: '',
	availableUnits: '',
	totalUnits: '',
	availabilityStatus: 'available',
	establishmentYear: '',
	availableUnitTypes: [],
	amenities: [],
};

export default function EditSociety({ open, onClose, onSave, initialSociety }) {
	const [society, setSociety] = React.useState({
		...emptySociety,
		name: initialSociety?.title ?? '',
		imageUrl: initialSociety?.img ?? '',
		managerName: initialSociety?.managerName ?? '',
		managerEmail: initialSociety?.managerEmail ?? '',
		managerContactNumber: initialSociety?.managerContactNumber ?? '',
		...initialSociety,
	});

	const handleChange = (event) => {
		const { name, value } = event.target;
		setSociety((currentSociety) => ({ ...currentSociety, [name]: value }));
	};

	const handleMultiValueChange = (event) => {
		const { name, value } = event.target;
		setSociety((currentSociety) => ({
			...currentSociety,
			[name]: typeof value === 'string' ? value.split(',') : value,
		}));
	};

	const handleAmenityChange = (event) => {
		const { value, checked } = event.target;
		setSociety((currentSociety) => ({
			...currentSociety,
			amenities: checked
				? [...currentSociety.amenities, value]
				: currentSociety.amenities.filter((amenity) => amenity !== value),
		}));
	};

	const handleSubmit = (event) => {
		event.preventDefault();
		onSave?.(society);
		onClose?.();
	};

	return (
		<Dialog
			open={open}
			onClose={onClose}
			aria-labelledby="edit-society-title"
			fullWidth
			maxWidth="sm"
		>
			<form onSubmit={handleSubmit}>
				<DialogTitle id="edit-society-title">Edit Society</DialogTitle>
				<DialogContent>
					<DialogContentText sx={{ mb: 2 }}>
						Update the society details below.
					</DialogContentText>
					<Stack spacing={2}>
						<TextField
							label="Society Name"
							name="name"
							value={society.name}
							onChange={handleChange}
							required
							fullWidth
						/>
						<TextField
							label="Society Image URL"
							name="imageUrl"
							type="url"
							value={society.imageUrl}
							onChange={handleChange}
							helperText="Add a publicly accessible image URL"
							required
							fullWidth
						/>
						<TextField
							label="Address"
							name="address"
							value={society.address}
							onChange={handleChange}
							required
							fullWidth
							multiline
							minRows={2}
						/>
						<TextField
							label="Summary"
							name="summary"
							value={society.summary}
							onChange={handleChange}
							fullWidth
							multiline
							minRows={3}
						/>
						<DialogContentText>Manager Contact Information</DialogContentText>
						<Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
							<TextField
								label="Manager Name"
								name="managerName"
								value={society.managerName}
								onChange={handleChange}
								required
								fullWidth
							/>
							<TextField
								label="Manager Email"
								name="managerEmail"
								type="email"
								value={society.managerEmail}
								onChange={handleChange}
								required
								fullWidth
							/>
						</Stack>
						<FormControl fullWidth required>
							<InputLabel id="edit-availability-status-label">Unit Availability Status</InputLabel>
							<Select
								labelId="edit-availability-status-label"
								name="availabilityStatus"
								value={society.availabilityStatus}
								onChange={handleChange}
								label="Unit Availability Status"
							>
								<MenuItem value="available">Available</MenuItem>
								<MenuItem value="not_available">Occupied</MenuItem>
							</Select>
						</FormControl>
						<TextField
							label="Manager Contact Number"
							name="managerContactNumber"
							type="tel"
							value={society.managerContactNumber}
							onChange={handleChange}
							inputProps={{ inputMode: 'tel' }}
							required
							fullWidth
						/>
						<Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
							<TextField
								label="Available Units"
								name="availableUnits"
								type="number"
								value={society.availableUnits}
								onChange={handleChange}
								inputProps={{ min: 0 }}
								required
								fullWidth
							/>
							<TextField
								label="Total Units"
								name="totalUnits"
								type="number"
								value={society.totalUnits}
								onChange={handleChange}
								inputProps={{ min: 0 }}
								required
								fullWidth
							/>
						</Stack>
						<TextField
							label="Establishment Year"
							name="establishmentYear"
							type="number"
							value={society.establishmentYear}
							onChange={handleChange}
							inputProps={{ min: 1800, max: new Date().getFullYear() }}
							required
							fullWidth
						/>
						<FormControl fullWidth>
							<InputLabel id="edit-unit-types-label">Available Unit Types</InputLabel>
							<Select
								labelId="edit-unit-types-label"
								name="availableUnitTypes"
								multiple
								value={society.availableUnitTypes}
								onChange={handleMultiValueChange}
								label="Available Unit Types"
							>
								{unitTypeOptions.map((unitType) => (
									<MenuItem key={unitType} value={unitType}>
										{unitType}
									</MenuItem>
								))}
							</Select>
						</FormControl>
						<FormControl component="fieldset" variant="standard">
							<InputLabel shrink>Amenities</InputLabel>
							<FormGroup row sx={{ pt: 2 }}>
								{amenityOptions.map((amenity) => (
									<FormControlLabel
										key={amenity}
										control={
											<Checkbox
												value={amenity}
												checked={society.amenities.includes(amenity)}
												onChange={handleAmenityChange}
											/>
										}
										label={amenity}
									/>
								))}
							</FormGroup>
						</FormControl>
					</Stack>
				</DialogContent>
				<DialogActions>
					<Button onClick={onClose}>Cancel</Button>
					<Button type="submit" variant="contained">Save Changes</Button>
				</DialogActions>
			</form>
		</Dialog>
	);
}
