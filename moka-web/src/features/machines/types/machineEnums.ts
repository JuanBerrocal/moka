
export const MachineType = {
    Unknown: 0,
    Machine: 1,
    Grinder: 2,
    Vending: 3,
    Capsule: 4,
    Juicer: 5,
    Warmer: 6,
    Refrigerator: 7,
    Dispenser: 8,
    Cabinet: 9,
    Office: 10,
    Others: 99,
} as const;

export const MachineState = {
    Unknown: 0,
    Ready: 1,
    NeedsReview: 2,
    Useless: 3,
} as const;

export type MachineType =
    (typeof MachineType)[keyof typeof MachineType];

export type MachineState =
    (typeof MachineState)[keyof typeof MachineState];